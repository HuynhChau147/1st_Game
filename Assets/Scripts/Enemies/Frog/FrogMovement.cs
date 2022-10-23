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
    public float jumpCooldown = 1; // 1sec for e.g.
    private float currentCooldown;

    [SerializeField] Rigidbody2D m_player;
    Rigidbody2D m_frog;
    [SerializeField] public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        m_frog = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player_Health = GetComponent<Health>();
        currentCooldown = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown > 0) // If jump not in cooldown
        {
            currentCooldown = jumpCooldown; // Set current cooldown time to jumpCooldown
            if(OnGround == true)
            {
                Jumping();
            }
        }
        else currentCooldown -= Time.deltaTime; // Reduce cooldown over time
        Debug.Log(currentCooldown);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.collider.tag == "Ground"){
                OnGround = true;
                animator.SetBool("Is Jump",false);
        }
        if(col.collider.tag == "Player" ){
            m_player.GetComponent<Health>().TakeDame(dame); 
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
            // transform.Translate(target*transform.localScale.x,0,0);
            OnGround = false;
            animator.SetBool("Is Jump",true);
        }

    private void OnTriggerEnter2D(Collider2D colTri) {
        if(colTri.CompareTag("Frog_Zone")){
            dir = -dir;
        }
    }

    private void FlipAnimator () {
        m_FacingRight = !m_FacingRight;

		Vector3 theScale = this.transform.localScale;
		theScale.x *= -1;
		this.transform.localScale = theScale;
    }
}
