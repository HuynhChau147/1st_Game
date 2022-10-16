using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Attribute of player
    private float horizontalMove;           
    private float verticalMove;
    private bool Jumping = false;
    private bool m_FacingRight = true;
    private bool OnGround;
    private bool canDash = true;
    private bool isDashing;
    private float dashingTime = 0.5f;
    private float dashingCooldown = 2f;
    // [SerializeField] public Sprite mySprite;

    // SerializeField to custom on Unity Editor
    private float MoveSpeed = 10f;
    [SerializeField] private float JumpForce;
    [SerializeField] private float dashingPower = 12f;
    [SerializeField] public Animator animator;

    Rigidbody2D m_player;
    

    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponent<Rigidbody2D>();
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
    const string PLAYER_DASHING = "Player_Dashing";

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
