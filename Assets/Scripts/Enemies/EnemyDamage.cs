using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] private Rigidbody2D m_player;

    protected void OnTriggerEnter2D(Collider2D colTri) 
    {
        if(colTri.tag == "Player")
        {
            colTri.GetComponent<Health>().TakeDame(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.collider.tag == "Player" )
        {
            m_player.GetComponent<Health>().TakeDame(damage); 
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
}
