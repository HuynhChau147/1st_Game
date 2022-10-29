using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public Rigidbody2D m_player;
    public Rigidbody2D m_Enemy;
    public GameObject enemy;
    public float flashTime;
    Color origionalColor;
    public SpriteRenderer spriteRender;

    // Start is called before the first frame update
    void Start()
    {
        origionalColor = spriteRender.color;
        currentHealth = maxHealth;
        m_player = GetComponent<Rigidbody2D>();
        m_Enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyTakeDame(int damage)
    {

        currentHealth -= damage;
        
        Debug.Log(currentHealth);

        if(currentHealth > 0)
        {
            FlashRed();
        }

        if(currentHealth <= 0 )
        {
            Debug.Log(m_Enemy.name + " dead");
            GetComponent<FrogMovement>().enabled = false;
            GetComponent<FrogMovement>().animator.SetTrigger("Dead");
            Destroy(enemy,0.5f);
        }
    }

    void FlashRed()
    {
        spriteRender.color = Color.red;
        Invoke("ResetColor", flashTime);
    }
    void ResetColor()
    {
        spriteRender.color = origionalColor;
    }


}
