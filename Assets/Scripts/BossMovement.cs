using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private int bossState = 0;
    private float movementSpeed = 2f;

    private Vector2 movementDirection;

    private Rigidbody2D parentRb;

    private int xDir = -1; // -1 = left, 1 = right
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentRb = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {    
        parentRb.linearVelocity = new Vector2(xDir * movementSpeed, parentRb.linearVelocity.y);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered has a specific tag
        
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("BossCollider"))
        {   
            if(bossState == 0)
            {  
                xDir *= -1;
            }
        }
    }
}
