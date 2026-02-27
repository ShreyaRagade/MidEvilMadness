using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float upVelocity = 20f;
    private Rigidbody2D rb; 
    public Health health;

    void Start(){
        rb = GetComponent<Rigidbody2D>();   
    }
    void Update()
    {
        if(health.healthAmount == 0)
        {
            Destroy(gameObject);
            Debug.Log("You Died!");
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        //Debug.Log(other.tag);
        if((other.CompareTag("Enemy") || other.CompareTag("EnemyAttack")) && other.gameObject.name != "Enemy Delete Collider"  ){
            rb = GetComponent<Rigidbody2D>();      

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, upVelocity);
            health.healthAmount -= 10;
            Debug.Log(health.healthAmount);
        }
        
    }
}
