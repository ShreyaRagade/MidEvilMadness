using UnityEngine;

public class Respawn : MonoBehaviour
{
    private int playerHealth;
    public Transform respawnPoint;
    public GameObject Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = Health.HealthInstance.healthAmount;
        Player = GameObject.FindWithTag("Player");
        respawnPoint = GameObject.FindWithTag("Respawn").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Health.HealthInstance.healthAmount <= 0)
         {
             Player.transform.position = respawnPoint.position;
             Health.HealthInstance.healthAmount = 100;
             if(Collectables.coins >= 2)
              {
                Collectables.coins -= 2;
              }
                else
                 {
                  Collectables.coins = 0;
                 }
                Debug.Log("Player respawned! Health reset to 100. Coins: " + Collectables.coins);
              }
         }
    }

