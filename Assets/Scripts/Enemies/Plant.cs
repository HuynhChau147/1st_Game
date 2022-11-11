using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] Bullet;
    private float cooldownTimer;

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

    private void Update() {
        cooldownTimer += Time.deltaTime;

        if(cooldownTimer >= attackCoolDown)
        {
            Attack();
        }
    }

}
