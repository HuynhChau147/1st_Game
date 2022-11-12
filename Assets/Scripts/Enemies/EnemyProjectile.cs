using UnityEngine;

public class EnemyProjectile : EnemyDamage // Inhertance TakeDame on player method - OnTriggerEnter2D //>
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;
    private float dir;
    
   public void ActiveProjectile()
    {
        lifeTime = 0;
        gameObject.SetActive(true);
    }

    private void Update() {
        dir = gameObject.transform.localScale.x;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(-dir*movementSpeed, 0, 0);
        
        lifeTime += Time.deltaTime;
        if(lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D colTri) 
    {
        if(colTri.CompareTag("Player"))
        {
            colTri.GetComponent<Health>().TakeDame(damage);
            gameObject.SetActive(false);
        }
    }
}
