using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public GameObject enemy;
    public Rigidbody2D m_enemy;
    public float flashTime;
    Color origionalColor;
    public SpriteRenderer spriteRender;
    private bool isTookDame;

    // Start is called before the first frame update
    void Start()
    {
        origionalColor = spriteRender.color;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void EnemyTakeDame(int damage)
    {
        if(maxHealth == 0)
        {
            return;
        }

        currentHealth -= damage;

        if(currentHealth > 0)
        {
            isTookDame = true;
            Flash();
        }

        if(currentHealth <= 0 )
        {
            Debug.Log(enemy.name + " dead");
            enemy.GetComponent<Animator>().SetTrigger("Dead");
            Destroy(enemy,0.5f);
        }
    }

    private void Flash()
    {
        spriteRender.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    void ResetColor()
    {
        spriteRender.color = origionalColor;
        isTookDame = false;
    }

    public bool getIsTookDame()
    {
        return isTookDame;
    }
}
