using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Attribute of player
    private float horizontalMove;           
    private float verticalMove;
    private float dashingTime = 0.5f;
    private float dashingCooldown = 2f;
    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private float slopeSideAngle;
    private float MoveSpeed = 10f;
    private float SlopeAngle;

    private bool Jumping = false;
    private bool m_FacingRight = true;
    private bool OnGround;
    private bool canDash = true;
    private bool isDashing;
    private bool isOnSlope;
    
    private Vector2 colliderSize;
    private Vector2 tempVelocity;
    private CapsuleCollider2D CapCol2D;
    
    private Vector2 slopeNormalPerp;

    // SerializeField to custom on Unity Editor
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D withFriction;
    [SerializeField] private float slopeForce;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private float JumpForce;
    [SerializeField] private float dashingPower = 12f;
    [SerializeField] public Animator animator;

    Rigidbody2D m_player;
    

    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponent<Rigidbody2D>();
        CapCol2D = GetComponent<CapsuleCollider2D>();
        colliderSize = CapCol2D.size;
        animator = GetComponent<Animator>();
        // this.GetComponent<SpriteRenderer>().sprite = mySprite;
        animator.SetBool("Is Jump", false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Jump");
        animator.SetFloat("Move Speed",Mathf.Abs((horizontalMove)));
        
        // Debug.Log("Test);
        // Debug.Log(OnGround);
        // Debug.Log(verticalMove);
        // Debug.Log(animator.GetBool("Is Jump"));
    }

    //Animation State

    private void FixedUpdate() {
        if(isDashing){
            return;
        }
        Move();
        if(Input.GetButtonDown("Jump") && OnGround)
        {
            Debug.Log("Jump");
            Jump();
        }
        if(Input.GetKeyDown(KeyCode.Z) && canDash){
            StartCoroutine(Dash());
        }
        SlopeCheck();
    }

    private void SlopeCheck(){
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);
        SlopeCheckVertical(checkPos);
        SlopeCheckHorizontal(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos){
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance,whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance,whatIsGround);

        if(slopeHitFront){
            Debug.Log("slope hit front");
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if(slopeHitBack){
            Debug.Log("slope hit back");
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else{
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }

    private void SlopeCheckVertical(Vector2 checkPos){
        RaycastHit2D hit2D = Physics2D.Raycast(checkPos, Vector2.down,slopeCheckDistance,whatIsGround);
        

        if(hit2D){
            slopeNormalPerp = Vector2.Perpendicular(hit2D.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit2D.normal, Vector2.up);
            if(slopeDownAngle != slopeDownAngleOld){
                isOnSlope = true;
                Debug.Log("On Slope");

            }
            slopeDownAngleOld = slopeDownAngle;
            Debug.DrawRay(hit2D.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit2D.point, hit2D.normal, Color.green);
        }
    }

    private void Jump () {
         m_player.AddForce(new Vector2(0f,JumpForce),ForceMode2D.Impulse);
         animator.SetBool("Is Jump",true);
        //  Debug.Log(verticalMove*JumpForce);
         OnGround = false;
        // transform.Translate(horizontalMove*MoveSpeed*Time.fixedDeltaTime,verticalMove*JumpForce*Time.fixedDeltaTime,0);
    }


    private void Move () {
        transform.Translate(horizontalMove*MoveSpeed*Time.deltaTime,0,0);
        if(!m_FacingRight && horizontalMove > 0){
            FlipAnimator();
        }
        else if (m_FacingRight && horizontalMove <0){
            FlipAnimator();
        }
        if(horizontalMove != 0){
            m_player.sharedMaterial = noFriction;
        }
        else{
            m_player.sharedMaterial = withFriction;
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.collider.tag == "Ground"){
                OnGround = true;
                animator.SetBool("Is Jump", false);
                Debug.Log("On Ground");
        }
    }

    private void FlipAnimator () {
        m_FacingRight = !m_FacingRight;

		Vector3 theScale = this.transform.localScale;
		theScale.x *= -1;
		this.transform.localScale = theScale;
    }

    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        float  originalGravity = m_player.gravityScale;
        m_player.gravityScale = 0f;
        m_player.velocity = new Vector2(transform.localScale.x *dashingPower,0f);
        animator.SetBool("Is Dash",isDashing);
        yield return new WaitForSeconds(dashingTime);
        m_player.velocity = new Vector2(0f,0f);
        m_player.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("Is Dash",isDashing);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        Debug.Log(m_player.transform.localPosition.x);
    }

}