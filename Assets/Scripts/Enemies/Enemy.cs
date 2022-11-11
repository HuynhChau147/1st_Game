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

    [SerializeField] protected float damage;

    // Start is called before the first frame update
    void Start()
    {
        origionalColor = spriteRender.color;
        currentHealth = maxHealth;
        m_player = GetComponent<Rigidbody2D>();
        m_Enemy = GetComponent<Rigidbody2D>();
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
            Flash();
        }

        if(currentHealth <= 0 )
        {
            Debug.Log(m_Enemy.name + " dead");
            // enGetComponent<FrogMovement>().enabled = false;
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
    }

    protected void OnTriggerEnter2D(Collider2D colTri) {
        if(colTri.tag == "Player")
        {
            colTri.GetComponent<Health>().TakeDame(damage);
        }
    }
}
