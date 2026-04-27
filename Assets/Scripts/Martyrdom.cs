using UnityEngine;

public class Martyrdom : MonoBehaviour
{
    private Vector2 movementDirection;

    private Rigidbody2D parentRb;
    private float upVelocity = 10f;
    private bool justMade = false;

    public GameObject targetObject;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentRb = GetComponent<Rigidbody2D>();   
        parentRb.linearVelocity = new Vector2(0, upVelocity);
        //Debug.Log("MADE MA");
        justMade = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision){
        //Debug.Log("Collide M | " + collision.gameObject.CompareTag("Floor") + " | " + parentRb.linearVelocity.y + " | " + justMade);
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("DrierEnemy") || collision.gameObject.CompareTag("Enemy")) {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("Floor") && parentRb.linearVelocity.y <= 0f)
        {
            //Debug.Log("boomish!");
            GameObject clone = Instantiate(targetObject, transform.position, transform.rotation);
            Destroy(gameObject);

        }
        justMade = false;
        //Debug.Log("justMade is now false");
        //KABOOM
        //Debug.Log("BOOM?");
        //Destroy(parentRb);
    }

}
