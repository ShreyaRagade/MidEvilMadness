using UnityEngine;

public class Hazard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int hazardDamage = 10;

    void Start()
    {

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
        }
    }
}
