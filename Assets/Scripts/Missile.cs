using UnityEngine;

public class Missile : MonoBehaviour
{
    private Vector2 movementDirection;

    private Rigidbody2D parentRb;
    public float martyrdomUpVelocity = 10f; 
    private bool justMade = false;

    public GameObject targetObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentRb = GetComponent<Rigidbody2D>();   
        justMade = true;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("DrierEnemy") || collision.gameObject.CompareTag("Enemy")) {
            //Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("boomish!");
            GameObject clone = Instantiate(targetObject, transform.position, transform.rotation);
            Destroy(gameObject);

        }
        justMade = false;
    }

}
