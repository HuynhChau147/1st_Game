using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Attribute of player
    private float horizontalMove;
    private float verticalMove;
    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private float slopeSideAngle;
    private float MoveSpeed = 12f;
    private float SlopeAngle;
    private bool Jumping = false;
    private bool Landing;
    private bool m_FacingRight = true;
    private bool OnGround;
    public bool KnockFromRight;
    public float KBFroce;
    public float KBCounter;
    public float KBTotalTime;

    private Vector2 colliderSize;
    private Vector2 tempVelocity;
    private CapsuleCollider2D CapCol2D;
    private Vector2 slopeNormalPerp;
    BoxCollider2D myFeet;
    
    // Private SerializeField to custom on Unity Editor
    [SerializeField] public AudioSource audioSrc;
    [SerializeField] public AudioClip jumpSound;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float JumpForce;
    [SerializeField] public Animator animator;

    Rigidbody2D m_player;


    // Start is called before the first frame update
    void Start()
    {
        myFeet = GetComponent<BoxCollider2D>();
        m_player = GetComponent<Rigidbody2D>();
        CapCol2D = GetComponent<CapsuleCollider2D>();
        colliderSize = CapCol2D.size;
        animator = GetComponent<Animator>();
        animator.SetBool("Is Jump", false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Jump");
        animator.SetFloat("Move Speed", Mathf.Abs((horizontalMove)));
        if (Input.GetButtonDown("Jump") && OnGround)
        {
            Jumping = true;
        }
    }

    //Animation State

    private void FixedUpdate()
    {
        if (KBCounter <= 0)
        {
            Move();
        }
        else
        {
            if(KnockFromRight == true)
            {
                m_player.velocity = new Vector2 (-KBFroce,KBFroce);
            }
            if(KnockFromRight == false)
            {
                m_player.velocity = new Vector2 (KBFroce,KBFroce);
            }
            KBCounter -= Time.deltaTime;
        }
        if (Jumping == true && Landing == false)
        {
            Debug.Log("Jump");
            Jump();
        }
    }

    private void Jump()
    {
        m_player.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        animator.SetBool("Is Jump", true);
        //  Debug.Log(verticalMove*JumpForce);
        OnGround = false;
        Landing = true;
        if (audioSrc && jumpSound)
        {
            audioSrc.PlayOneShot(jumpSound);
        }
        // transform.Translate(horizontalMove*MoveSpeed*Time.fixedDeltaTime,verticalMove*JumpForce*Time.fixedDeltaTime,0);
    }


    private void Move()
    {
        transform.Translate(horizontalMove * MoveSpeed * Time.deltaTime, 0, 0);
        if (!m_FacingRight && horizontalMove > 0)
        {
            FlipAnimator();
        }
        else if (m_FacingRight && horizontalMove < 0)
        {
            FlipAnimator();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Floor")))
        {
            OnGround = true;
            Jumping = false;
            Landing = false;
            animator.SetBool("Is Jump", false);
            animator.SetBool("Jump Attack", false);
            Debug.Log("On Ground");
        }
    }

    private void FlipAnimator()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }

    public bool getJumping()
    {
        return Jumping;
    }
}
