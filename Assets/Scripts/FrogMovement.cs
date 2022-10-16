using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    private float speed = 2f;
    private float JumpForce = 5f;
    private bool OnGround;
    private float dir = 1;

    [SerializeField] Rigidbody2D m_player;
    Rigidbody2D m_frog;

    // Start is called before the first frame update
    void Start()
    {
        m_frog = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_frog.transform.localPosition != m_player.transform.localPosition && OnGround == true){
            Jumping();
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.collider.tag == "Ground"){
                OnGround = true;
        }
    }

    private void Jumping(){
            m_frog.AddForce(new Vector2(speed*dir,JumpForce),ForceMode2D.Impulse);
            // transform.Translate(target*transform.localScale.x,0,0);
            OnGround = false;
        }

    private void OnTriggerEnter2D(Collider2D colTri) {
        if(colTri.CompareTag("Frog_Zone")){
            dir = -dir;
            Debug.Log("Frog doi dau");
        }
        Debug.Log(dir);
    }
}
