//This function is applied to the enemy's box on top of it, and when it co
using UnityEngine;

public class EnemyDelete : MonoBehaviour 
{
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("DIE DIE DIE HAHAHAHA");
            Destroy(gameObject);
        }
    }

}
