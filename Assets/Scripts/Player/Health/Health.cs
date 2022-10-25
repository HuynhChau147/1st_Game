using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth{ get; private set; }
    private Animator anim;
    private bool dead;
    public Rigidbody2D m_player;

    [SerializeField] private float startingHealth;

    private void Awake() {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDame(float damage){
        currentHealth = Mathf.Clamp(currentHealth - damage, 0 , startingHealth);

        if(currentHealth > 0){
            // Take dame
            anim.SetTrigger("hurt");
            // m_player.OnBecameInvisible() {
                
            // }
        }

        else{
            // Dead
            if(!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    public void AddHealth(float healthValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthValue, 0 , startingHealth);
    }
}
