using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float JumpForce;
    [SerializeField] private Animator anim;
     
    private void OnTriggerEnter2D(Collider2D colTri) {
        if(colTri.CompareTag("Player"))
        {
            anim.SetTrigger("Actived");
            colTri.GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }
}
