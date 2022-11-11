using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    private float speed = 2f;
    private float JumpForce = 5f;
    private bool OnGround;
    private float dir = 1;
    private bool m_FacingRight = true;
    private Health player_Health;
    private float dame = 1;
    public float jumpCooldown = 1;
    private float currentCooldown;
    public BoxCollider2D col2D;
    [SerializeField] Rigidbody2D m_player;
    Rigidbody2D m_frog;
    [SerializeField] public Animator animator;
    private bool mustTurn = false;
    public Transform groundCheckPos;
    public LayerMask groundCheckLayer;
    public float groundCheckRadius;

    // Start is called before the first frame update
    void Start()
    {
        m_frog = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player_Health = GetComponent<Health>();
        Physics2D.IgnoreLayerCollision(9,9,true);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown < 0) // If jump not in cooldown
        {
            if(OnGround == true)
            {
                if(mustTurn)
                {
                    dir *= -1;
                    mustTurn = false;
                }
                currentCooldown = jumpCooldown; // Set current cooldown time to jumpCooldown
                Jumping();
            }
        }
        else currentCooldown -= Time.deltaTime; // Reduce cooldown over time

        mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, groundCheckLayer);
        Debug.Log(mustTurn); 
        Debug.Log(dir);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        
        if(col.collider.tag == "Ground")
        {
                OnGround = true;
                animator.SetBool("Is Jump",false);
        }

        if(col.collider.tag == "Player" )
        {
            m_player.GetComponent<Health>().TakeDame(dame); 
            m_player.GetComponent<PlayerMovement>().Player_Invincible();
            m_player.GetComponent<PlayerMovement>().KBCounter = m_player.GetComponent<PlayerMovement>().KBTotalTime;
            if(col.transform.position.x <= transform.position.x)
            {
                m_player.GetComponent<PlayerMovement>().KnockFromRight = true;
            }
            if(col.transform.position.x > transform.position.x)
            {
                m_player.GetComponent<PlayerMovement>().KnockFromRight = false;
            }
            Debug.Log("Took Dame");
        }
    }

    private void Jumping(){

        m_frog.AddForce(new Vector2(speed*dir,JumpForce),ForceMode2D.Impulse);
        if(m_FacingRight && dir > 0){
            FlipAnimator();
        }
        else if (!m_FacingRight && dir < 0){
                FlipAnimator();
        }
        OnGround = false;
        animator.SetBool("Is Jump",true);
        }

    private void FlipAnimator () {
        m_FacingRight = !m_FacingRight;
		Vector3 theScale = m_frog.transform.localScale;
		theScale.x *= -1;
		m_frog.transform.localScale = theScale;
    }

    private void OnDrawGizmosSelected() {
        if(groundCheckPos == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }
}
