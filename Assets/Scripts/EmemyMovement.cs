using UnityEngine;

public class EmemyMovement : MonoBehaviour
{
    private float movementSpeed = 4f;

    private Vector2 movementDirection;

    //private Rigidbody2D rb; 
    private Rigidbody2D parentRb;

    private Transform childTransform;
    private Vector3 positionLeft = new Vector3(-0.1f, 0, 0);
    private Vector3 positionRight = new Vector3(0.1f, 0, 0);


    public int xEDir = -1; // -1 = left, 1 = right
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //rb = transform.GetComponent<Rigidbody2D>();
        parentRb = transform.parent.GetComponent<Rigidbody2D>();
        parentRb.linearVelocity = new Vector2(xEDir * movementSpeed, 0);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        //rb.linearVelocity = new Vector2(0, xEDir * movementSpeed);
        //parentRb.linearVelocity =  new Vector2(0, xEDir * movementSpeed);
    
        if (PauseController.IsGamePaused)
        {
            return;
        }
        parentRb.linearVelocity = new Vector2(xEDir * movementSpeed, parentRb.linearVelocity.y);

        //movementDirection = new Vector2(movementSpeed*xEDir, 0f); 
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered has a specific tag
        if (other.gameObject.CompareTag("Floor"))// (enemy.CompareTag("floor"))
        {
            //Debug.Log("Enemy Movement ");
            foreach (Transform child in transform.parent)
            {
                if (child.transform.localPosition == positionLeft)
                {
                    child.transform.localPosition = positionRight;
                }
                else if(child.transform.localPosition == positionRight)
                {
                    child.transform.localPosition = positionLeft;
                }
            }
            
            xEDir *= -1;
        }
    }
}
