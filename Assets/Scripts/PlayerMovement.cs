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
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    // SerializeField to custom on Unity Editor
    [SerializeField] private float MoveSpeed;
    // [SerializeField] 
    private float JumpForce = 3f;
    [SerializeField] private float dashingPower;
    [SerializeField] public Animator animator;
    [SerializeField] private TrailRenderer trailRenderer;

    Rigidbody2D m_player;
    

    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponent<Rigidbody2D>();
        animator.SetBool("Is Jump", false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Jump");
        animator.SetFloat("Move Speed",Mathf.Abs((horizontalMove)));
        Move(horizontalMove,MoveSpeed,m_FacingRight);
        if(Input.GetButtonDown("Jump") && OnGround)
        {
            Jump();
        }
        // Debug.Log(OnGround);
        // Debug.Log(verticalMove);
        // Debug.Log(animator.GetBool("Is Jump"));
    }


    private void Jump () {

         transform.Translate(0,verticalMove*JumpForce,0);
         animator.SetBool("Is Jump",true);
        //  Debug.Log(verticalMove*JumpForce);
         OnGround = false;
        // transform.Translate(horizontalMove*MoveSpeed*Time.fixedDeltaTime,verticalMove*JumpForce*Time.fixedDeltaTime,0);
    }


    private void Move (float horizontalMove,float MoveSpeed, bool m_FacingRight) {
        transform.Translate(horizontalMove*MoveSpeed*Time.fixedDeltaTime,0,0);
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

}
