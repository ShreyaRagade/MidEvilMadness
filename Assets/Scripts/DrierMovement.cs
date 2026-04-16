using UnityEngine;

public class DrierMovement : MonoBehaviour
{
    private float movementSpeed = 2f;

    private Vector2 movementDirection;

    //private Rigidbody2D rb; 
    private Rigidbody2D parentRb;

    private Transform childTransform;
    private Vector3 positionLeft = new Vector3(-0.1f, 0, 0);
    private Vector3 positionRight = new Vector3(0.1f, 0, 0);


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
        //rb.linearVelocity = new Vector2(xDir * movementSpeed, rb.linearVelocity.y);
        parentRb.linearVelocity = new Vector2(xDir * movementSpeed, parentRb.linearVelocity.y);
    
        //parentRb.linearVelocity = new Vector2(xDir * movementSpeed, parentRb.linearVelocity.y);

        //movementDirection = new Vector2(movementSpeed*xDir, 0f); 
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered has a specific tag
        if (other.gameObject.CompareTag("Floor"))// (enemy.CompareTag("floor"))
        {
            Debug.Log("Drier Movement ");
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
            
            xDir *= -1;
            //rb.linearVelocity = new Vector2(xDir * movementSpeed, 0);
            // parentRb.linearVelocity = new Vector2(xDir * movementSpeed, 0);
            //transform.Translate(Vector3.up * 100);
            //transform.parent.Translate(Vector3.up * 5);
            
            //transform.parent

            // Perform actions like playing sound, loading scene, etc.
            //Vector3 parentPosition = transform.parent.position

        }
    }

    /*
    ontriggerenter



    */
}
