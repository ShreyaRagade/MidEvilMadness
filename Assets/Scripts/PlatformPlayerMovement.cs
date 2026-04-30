using UnityEngine;

public class PlatformPlayerMovement : MonoBehaviour
{
    public Movement movement;
    float friction = 0.2f;
    
    // 1. Just declare the variable here
    private Rigidbody2D rb; 

    void Start()
    {
        // 2. Assign the component here
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player Friction: " + friction);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player on Platform");
            if(!movement.isMoving)
            {
                rb.sharedMaterial.friction = 100f; 
            }else
            {
                rb.sharedMaterial.friction = 0.2f; 
            }

            friction = rb.sharedMaterial.friction;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Left Platform");
            rb.sharedMaterial.friction = 0.2f; 
            friction = rb.sharedMaterial.friction;
        }
    }
}
