using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollectible : MonoBehaviour
{
    [SerializeField] public AudioSource audioSrc;
    [SerializeField] public AudioClip collectSound;
    private int GemValue = 50;
    private int GemCounter = 1;
    [SerializeField] private Rigidbody2D m_player;

    private void OnTriggerEnter2D(Collider2D colTri) 
    {
        if(colTri.tag == "Player")
        {
            m_player.GetComponent<Player_Score>().AddGem(GemCounter);
            m_player.GetComponent<Player_Score>().AddScore(GemValue);
            if(audioSrc && collectSound)
            {
                audioSrc.PlayOneShot(collectSound);
            }
            gameObject.SetActive(false);
        }   
    }
}
