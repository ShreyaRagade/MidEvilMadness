using UnityEngine;

public class Hazard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int hazardDamage = 10;
    public GameObject respawnPoint;
    public GameObject Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        respawnPoint = GameObject.FindWithTag("Respawn");
        Debug.Log(Player.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health.HealthInstance.healthAmount -= hazardDamage;
            Player.transform.position = respawnPoint.transform.position;
        }
    }
}
