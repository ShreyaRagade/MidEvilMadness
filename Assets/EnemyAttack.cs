using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float upVelocity = 20f;
    private Rigidbody2D rb; 

    void Start(){
        Rigidbody2D rb = GetComponent<Rigidbody2D>();   
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            //Debug.Log("Enemy Player Collided");
            rb = GetComponent<Rigidbody2D>();   

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, upVelocity);
            //playerHeath -= 1;
            //maybe do death thing here?
        }
        
    }
}
