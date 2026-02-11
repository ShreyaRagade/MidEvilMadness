using UnityEngine;

public class Collectables : MonoBehaviour
{
    public static int coins = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("This is a collectable!");
            coins = coins + 1;
            Debug.Log("Coins: " + coins);
            Destroy(gameObject);
        }
    }
}
