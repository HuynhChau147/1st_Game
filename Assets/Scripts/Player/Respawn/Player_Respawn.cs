using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Respawn : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uIManager;

    private void Awake() {
        playerHealth = GetComponent<Health>();
        uIManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {   
        if(currentCheckpoint == null)
        {
            audioSource.Stop();
            uIManager.GameOver();
            return;
        }

        playerHealth.Respawn();
        transform.position = currentCheckpoint.position;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.transform.tag == "Checkpoint")
        {
            currentCheckpoint = col.transform;
            audioSource.PlayOneShot(checkpointSound);
            col.GetComponent<Collider2D>().enabled = false;
            col.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}