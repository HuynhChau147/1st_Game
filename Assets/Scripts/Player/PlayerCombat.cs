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

    [SerializeField] public AudioSource audioSrc;
    [SerializeField] public AudioClip AttackSound;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttactkTime)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                Attack();
                nextAttactkTime = Time.time + 1f/ attackRate;
            }
        }
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
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
