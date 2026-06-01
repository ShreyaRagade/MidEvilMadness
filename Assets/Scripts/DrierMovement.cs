using UnityEngine;

public class DrierMovement : MonoBehaviour
{
    private float movementSpeed = 4f;

    private Vector2 movementDirection;

    private Rigidbody2D parentRb;

    private Transform childTransform;
    private Vector3 positionLeft = new Vector3(-0.1f, 0, 0);
    private Vector3 positionRight = new Vector3(0.1f, 0, 0);

    public int xDDir = -1; // -1 = left, 1 = right
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentRb = transform.parent.GetComponent<Rigidbody2D>();
        parentRb.linearVelocity = new Vector2(xDDir * movementSpeed, 0);

    }

    // Update is called once per frame
    void Update()
    {
        parentRb.linearVelocity = new Vector2(xDDir * movementSpeed, parentRb.linearVelocity.y);
   }

    void OnTriggerEnter2D(Collider2D other)
    {
        //checks if the New Enemy collided with the Floor
        if (other.gameObject.CompareTag("Floor"))
        {
            //swaps the positions of the two colliders 
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
            //changes the direction of the New Enemy
            xDDir *= -1;
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }

}
