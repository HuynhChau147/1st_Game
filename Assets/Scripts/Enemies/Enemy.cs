using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float KBFroce;
    [SerializeField] private float KBCounter;
    [SerializeField] private float KBTotalTime;
    private bool KnockFromRight;
    public Rigidbody2D m_player;
    public Rigidbody2D m_Enemy;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponent<Rigidbody2D>();
        m_Enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
