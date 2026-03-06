using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TMP_Text healthText;
    private int playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = Health.HealthInstance.healthAmount;
        healthText.text = playerHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = Health.HealthInstance.healthAmount;
        healthText.text = "Health: " + playerHealth.ToString();
        //healthText.text = "Health:" + Health.HealthInstance.healthAmount;
    }
}
