using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float movementSpeed = 4f;
    private bool vert = false;
    private bool horz = false;

    private Vector2 movementDirection;

    //private Rigidbody2D rb; 
    private Rigidbody2D parentRb;
    public GameObject Platform;
    private bool playerOnPlatform = false;
    private GameObject child;
    


    private int xDir = -1; // -1 = left, 1 = right
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //rb = transform.GetComponent<Rigidbody2D>();
        parentRb = transform.parent.GetComponent<Rigidbody2D>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (vert)
        {
            parentRb.linearVelocity = new Vector2(parentRb.linearVelocity.x, xDir * movementSpeed);
        }
        else if (horz)
        {
            parentRb.linearVelocity =  new Vector2(xDir * movementSpeed, parentRb.linearVelocity.y);
        }
        if(playerOnPlatform)
        {
            
            // Move the player with the platform
            // Assuming the player has a Rigidbody2D component
            // You can adjust this to fit your specific player movement logic
            // For example, you might want to add the platform's velocity to the player's velocity
            // or set the player's position relative to the platform's position
            // Example: Adding platform's velocity to player's velocity
            Rigidbody2D playerRb = child.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                if (horz)
                {
                    playerRb.linearVelocity += parentRb.linearVelocity;
                }
            }

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered has a specific tag
        if (other.gameObject.CompareTag("Floor"))// (enemy.CompareTag("floor"))
        {            
            xDir *= -1;
        }

        if (other.gameObject.CompareTag("Vertical")){
            vert = true;
        }
        if (other.gameObject.CompareTag("Horizontal")){
            horz = true;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
            playerOnPlatform = true;
            child = transform.GetChild(0).gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            playerOnPlatform = false;
        }
    }
}
