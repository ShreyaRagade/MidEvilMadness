using UnityEngine;

public class Martyrdom : MonoBehaviour
{
    private Vector2 movementDirection;

    private Rigidbody2D parentRb;
    private float upVelocity = 20f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentRb = GetComponent<Rigidbody2D>();   
        parentRb.linearVelocity = new Vector2(0, upVelocity);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        
        //KABOOM
        Debug.Log("BOOM?");
        Destroy(parentRb);
    }

}
