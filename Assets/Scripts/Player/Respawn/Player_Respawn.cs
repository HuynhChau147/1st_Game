using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Respawn : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip checkpointSound;
    [SerializeField] private Transform startingPoint;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uIManager;
    private int currentLife;

    private void Awake() {
        playerHealth = GetComponent<Health>();
        uIManager = FindObjectOfType<UIManager>();
    }

    private void Update() {
        currentLife = GetComponent<LifeManager>().getLifeCounter();
    }

    public void CheckRespawn()
    {   
        if(currentLife == 0)
        {
            audioSource.Stop();
            uIManager.GameOver();
            return;
        }

        if(currentCheckpoint == null && currentLife > 0 )
        {
            playerHealth.Respawn();
            transform.position = startingPoint.position;
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
