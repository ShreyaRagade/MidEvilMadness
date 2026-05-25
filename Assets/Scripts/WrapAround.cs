using UnityEngine;

public class WrapAround : MonoBehaviour
{
    public GameObject Player;
    public GameObject leftBoundary;
    public GameObject rightBoundary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        leftBoundary = GameObject.FindWithTag("LeftCollider");
        rightBoundary = GameObject.FindWithTag("RightCollider");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(this.gameObject.CompareTag("LeftCollider"))
            {
                Player.transform.position = rightBoundary.transform.position;
            }
            else if(this.gameObject.CompareTag("RightCollider"))
            {
                Player.transform.position = leftBoundary.transform.position;
            }
        }
    }
}
