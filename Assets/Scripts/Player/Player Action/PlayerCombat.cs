using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttactkTime = 0f;
    public LayerMask enemyLayers;
    private bool JumpingStatus;

    [SerializeField] public AudioSource audioSrc;
    [SerializeField] public AudioClip AttackSound;

    // Update is called once per frame
    void Update()
    {
        JumpingStatus = this.GetComponent<PlayerMovement>().getJumping();
        if(Time.time >= nextAttactkTime)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                Attack();
                nextAttactkTime = Time.time + 1f/ attackRate;
            }

            if(Input.GetKeyDown(KeyCode.C) && JumpingStatus )
            {
                JumpAttack();
                nextAttactkTime = Time.time + 1f/ attackRate;
            }
        }
    }

    private void JumpAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        anim.SetBool("Is Jump", false);
        anim.SetBool("Jump Attack",true);
        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy.tag == "Player")
            {
                enemy.GetComponent<Enemy>().EnemyTakeDame(1);
                Debug.Log(enemy.GetComponent<Enemy>().getCurrentHealth());
            }
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        if(this.GetComponent<Health>().getHurtState())
        {
            return;
        }

        anim.SetTrigger("Attack");
        
        audioSrc.PlayOneShot(AttackSound);

         foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().EnemyTakeDame(1);
        }
    }

    private void OnDrawGizmosSelected() {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
