using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] Bullet;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject Zone;
    private float cooldownTimer;
    private bool canAttack;

    private void Attack()
    {
        cooldownTimer = 0;

        Bullet[FindBullet()].transform.position = firePoint.position;
        Bullet[FindBullet()].GetComponent<EnemyProjectile>().ActiveProjectile();
    } 

    private int FindBullet()
    {
        for(int i = 0; i < Bullet.Length; i++)
        {
            if(Bullet[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update() 
    {

        cooldownTimer += Time.deltaTime;
        canAttack = Zone.GetComponent<ZoneDetacting>().getAttackStatus();
        if(cooldownTimer >= attackCoolDown && canAttack)
        {
            anim.SetBool("Is Attack", canAttack);
            Attack();
        }
        else if(!canAttack)
        {
            anim.SetBool("Is Attack", canAttack);
        }

        if(gameObject.GetComponent<Enemy>().getIsTookDame())
        {
            anim.SetTrigger("hurt");
        }

    }

    public Animator getAnimator()
    {
        return this.anim;
    }
}
