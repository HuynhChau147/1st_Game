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

    [SerializeField] Rigidbody2D m_player;
    Rigidbody2D m_frog;
    [SerializeField] public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        m_frog = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(OnGround == true){
            Jumping();
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.collider.tag == "Ground"){
                OnGround = true;
                animator.SetBool("Is Jump",false);
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
            Debug.Log("Frog doi dau");
        }
        Debug.Log(dir);
    }

    private void FlipAnimator () {
        m_FacingRight = !m_FacingRight;

		Vector3 theScale = this.transform.localScale;
		theScale.x *= -1;
		this.transform.localScale = theScale;
    }
}
