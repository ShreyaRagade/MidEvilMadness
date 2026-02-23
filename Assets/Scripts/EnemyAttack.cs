using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float upVelocity = 20f;
    private Rigidbody2D rb; 

    void Start(){
        rb = GetComponent<Rigidbody2D>();   
    }

    private void OnTriggerEnter2D(Collider2D other){
        //Debug.Log(other.tag);
        if((other.CompareTag("Enemy") || other.CompareTag("EnemyAttack")) && other.gameObject.name != "Enemy Delete Collider"  ){
            rb = GetComponent<Rigidbody2D>();      

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, upVelocity);
            //playerHeath -= 1;
            //maybe do death thing here?
        }
        
    }
}
