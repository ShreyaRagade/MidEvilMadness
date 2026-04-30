using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float movementSpeed = 4f;
    private bool vert = false;
    private bool horz = false;

    private Vector2 movementDirection;

    //private Rigidbody2D rb; 
    private Rigidbody2D parentRb;



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
    }

    /*
    ontriggerenter



    */
}
