using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject m_Player;
    private bool isChase = false;
    public Transform startingPoint;
    public GameObject Zone;
    private bool m_FacingRight;

    // Update is called once per frame
    void Update()
    {
        isChase = Zone.GetComponent<ZoneDetacting>().getAttackStatus();
        if(m_Player == null)
        {
            return;
        }
        if(isChase == true)
        {
            Chase();
        }
        else if(isChase == false)
        {
            ReturnStartingPoint();
        }

        if (m_Player.GetComponent<Health>().getDeadState())
        {
            ReturnStartingPoint();
        }
        FlipAnimator();
    }

    private void Chase()
    {
        if(this.transform.position.x > m_Player.transform.position.x && m_Player != null)
        {
            m_FacingRight = true;
        }
        else if(this.transform.position.x < m_Player.transform.position.x && m_Player != null)
        {
            m_FacingRight = false;
        }
        transform.position = Vector2.MoveTowards(transform.position, m_Player.transform.position, speed * Time.deltaTime);
    }

    private void ReturnStartingPoint()
    {
        if(this.transform.position.x > startingPoint.position.x)
        {
            m_FacingRight = true;
        }
        else m_FacingRight = false;
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }

    private void FlipAnimator () {
        if(m_FacingRight)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }
}
