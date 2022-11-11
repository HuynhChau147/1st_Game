using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth{ get; private set; }
    private Animator anim;
    private bool dead;
    public Rigidbody2D m_player;
    [SerializeField] private Behaviour[] components;
    [SerializeField] private float startingHealth;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip DeadSound;
    [SerializeField] private float iFrameDuration;

    private void Awake() {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDame(float damage){
        currentHealth = Mathf.Clamp(currentHealth - damage, 0 , startingHealth);

        if(currentHealth > 0){
            // Take dame
            anim.SetBool("Is Jump",false);
            anim.SetTrigger("hurt");
        }

        else{
            // Dead
            if(!dead)
            {
                anim.SetBool("Is Jump",false);
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<CapsuleCollider2D>().enabled = false;
                m_player.gravityScale = 0;
                dead = true;
                audioSource.PlayOneShot(DeadSound);
            }
        }
    }

    public void AddHealth(float healthValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthValue, 0 , startingHealth);
    }

    public void Respawn(){
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Player_idle");
        GetComponent<CapsuleCollider2D>().enabled = true;
        m_player.gravityScale = 3;
        
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }
}
