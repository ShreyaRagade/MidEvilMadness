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
            Debug.Log(inShop);
            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        inShop = false;
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (inShop)
        {
            Debug.Log("Welcome To The Shop!");
            if(Collectables.coins > 5 && (Health.healthAmount < Health.MaxHealth))
            {
                Collectables.coins -= 5;
                Health.healthAmount += 10;
                if(Health.healthAmount > Health.MaxHealth)
                {
                    health.healthAmount = Health.MaxHealth;
                }
                Debug.Log("You now only have " + Collectables.coins + " Coins");
            }
            else
            {
                Debug.Log("Insufficient Funds! >:(");
            }
        }
    }
}