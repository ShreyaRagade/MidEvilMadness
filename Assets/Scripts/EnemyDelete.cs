//This function is applied to the enemy's box on top of it, and when it co
using UnityEngine;

public class EnemyDelete : MonoBehaviour 
{
    public GameObject targetObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D Rb = other.gameObject.GetComponent<Rigidbody2D>();
            if(Rb.linearVelocity.y < 0) //only deletes enemy if the player is moving downwards
            {
                if(gameObject.CompareTag("DrierEnemy")){
                    Destroy(transform.parent.gameObject);
                    //spawn martyrdom
                    Vector2 spawnPosition = transform.position;
                    //spawnPosition.y += 5f;
                   
                    GameObject clone = Instantiate(targetObject, spawnPosition + Vector2.up * 5f, transform.rotation);
                    
                    Debug.Log("ok Awesome");
                }
                else if(gameObject.CompareTag("Enemy")){
                    //Debug.Log("Enemy Dies");
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }
}
