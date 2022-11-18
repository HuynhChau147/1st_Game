using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] public AudioSource audioSrc;
    [SerializeField] public AudioClip collectSound;
    [SerializeField] private float healthValue;
    [SerializeField] private Rigidbody2D m_player;

    private void OnTriggerEnter2D(Collider2D colTri) 
    {
        if(colTri.tag == "Player")
        {
            m_player.GetComponent<Health>().AddHealth(healthValue);
            if(audioSrc && collectSound)
            {
                audioSrc.PlayOneShot(collectSound);
            }
            gameObject.SetActive(false);
        }   
    }
}
