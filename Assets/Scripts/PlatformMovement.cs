using UnityEngine;

public class MovingPlatform : MonoBehaviour 
{
    [SerializeField] private float movementSpeed = 4f;
    private bool vert = false;
    private bool horz = false;
    private int xDir = -1; // -1 = left, 1 = right

    private Rigidbody2D parentRb;
    private Rigidbody2D playerRb;
    private bool playerOnPlatform = false;

    void Start()
    {
        parentRb = transform.parent.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Keep non-physics trigger flags or direction switching logic here if needed
    }

    void FixedUpdate()
    {
        // 1. Move the platform safely using physics velocities
        if (vert) 
        {
            parentRb.linearVelocity = new Vector2(parentRb.linearVelocity.x, xDir * movementSpeed);
        } 
        else if (horz) 
        {
            parentRb.linearVelocity = new Vector2(xDir * movementSpeed, parentRb.linearVelocity.y);
        }

        // 2. Safely apply platform velocity adjustments to the player while riding
        if (playerOnPlatform && playerRb != null && horz)
        {
            // Set horizontal velocity directly to match the platform, maintaining the player's original vertical gravity/fall speed
            playerRb.linearVelocity += parentRb.linearVelocity;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            xDir *= -1;
        }
        if (other.gameObject.CompareTag("Vertical"))
        {
            vert = true;
        }
        if (other.gameObject.CompareTag("Horizontal"))
        {
            horz = true;
        }
        if (other.gameObject.CompareTag("Player")) 
        {
            playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerOnPlatform = true;
                
                // Optional: Remove SetParent if your player has its own Rigidbody movement.
                // Leaving it enabled can fight against direct Rigidbody velocity changes.
                other.transform.SetParent(this.transform); 
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            other.transform.SetParent(null);
            playerOnPlatform = false;
            playerRb = null;
        }
    }
}
