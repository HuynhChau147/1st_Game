using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPatrol : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.name == "Player")
        {
            col.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D col) {
        if(col.gameObject.name == "Player")
        {
            col.gameObject.transform.SetParent(null);
        }
    }
}
