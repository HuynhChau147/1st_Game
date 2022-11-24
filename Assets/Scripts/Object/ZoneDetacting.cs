using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDetacting : MonoBehaviour
{
    private bool canAttack;
    private void OnTriggerEnter2D(Collider2D colTri) {
        if(colTri.CompareTag("Player"))
        {
            Debug.Log("IN");
            canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D colTri) {
        if(colTri.CompareTag("Player"))
        {
            Debug.Log("OUT");
            canAttack = false;
        }
    }

    public bool getAttackStatus()
    {
        return this.canAttack;
    }
}
