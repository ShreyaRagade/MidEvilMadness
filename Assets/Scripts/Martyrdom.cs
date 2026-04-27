using UnityEngine;

public class Martyrdom : MonoBehaviour
{
    private Vector2 movementDirection;

    private Rigidbody2D parentRb;
    private float upVelocity = 10f;

    public GameObject targetObject;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentRb = GetComponent<Rigidbody2D>();   
        parentRb.linearVelocity = new Vector2(0, upVelocity);
        Debug.Log("MADE MA");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player")) {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("boomish!");
            GameObject clone = Instantiate(targetObject, transform.position, transform.rotation);
            //Destroy(transform.gameObject);

        }

        //KABOOM
        //Debug.Log("BOOM?");
        //Destroy(parentRb);
    }

}
