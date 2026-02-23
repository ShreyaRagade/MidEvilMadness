//This function is applied to the enemy's box on top of it, and when it co
using UnityEngine;

public class EnemyDelete : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D Rb = other.gameObject.GetComponent<Rigidbody2D>();
            if(Rb.linearVelocity.y < 0) //only deletes enemy if the player is moving downwards
            {
                //Debug.Log("Enemy Dies");
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
