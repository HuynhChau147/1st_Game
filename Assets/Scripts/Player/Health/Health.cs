using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float currentHealth{ get; private set; }
    private Animator anim;
    private bool dead;
    private bool isHurt;
    public Rigidbody2D m_player;
    [SerializeField] private Behaviour[] components;
    [SerializeField] private float startingHealth;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip DeadSound;
    [SerializeField] private float iFramesDuration = 0.3f;

    private void Awake() {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDame(float damage){
        currentHealth = Mathf.Clamp(currentHealth - damage, 0 , startingHealth);

        if(currentHealth > 0){
            // Take dame
            isHurt = true;
            anim.SetBool("Is Jump",false);
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }

        else{
            // Dead
            if(!dead)
            {
                anim.SetBool("Is Jump",false);
                anim.SetTrigger("die");
                GetComponent<LifeManager>().LostLife();
                m_player.gravityScale = 0;
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<CapsuleCollider2D>().enabled = false;
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

    private IEnumerator Invunerability()
    {
        yield return new WaitForSeconds(iFramesDuration);
        Physics2D.IgnoreLayerCollision(3,12, true);
        yield return new WaitForSeconds(iFramesDuration);
        Physics2D.IgnoreLayerCollision(3,12, false);
        isHurt = false;
    }

    public bool getDeadState()
    {
        return dead;
    }

    public bool getHutState()
    {
        return isHurt;
    }

    public void Dead()
    {
        anim.SetBool("Is Jump",false);
        anim.SetTrigger("die");
        GetComponent<LifeManager>().LostLife();
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        m_player.gravityScale = 0;
        dead = true;
        audioSource.PlayOneShot(DeadSound);
    }
}
