using UnityEngine;
using UnityEngine.InputSystem;

public class ShopButton : MonoBehaviour
{
    private bool inShop;
    public Collectables collectables;
    public Health health;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inShop = true;
            Debug.Log("Welcome To The Shop!");
            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        inShop = false;
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (inShop)
        {
            if(Collectables.coins >= 5 && (health.healthAmount < health.MaxHealth))
            {
                Collectables.coins -= 5;
                health.healthAmount += 10;
                if(health.healthAmount > health.MaxHealth)
                {
                    health.healthAmount = health.MaxHealth;
                }
                Debug.Log("You now have " + Collectables.coins + " Coins!");
                Debug.Log("You now have " + health.healthAmount + " HP!");
            }
            else if(Collectables.coins < 5)
            {
                Debug.Log("Insufficient Funds! >:(");
            }
            else
            {
                Debug.Log("Already at Max Health");
            }
        }
    }
}