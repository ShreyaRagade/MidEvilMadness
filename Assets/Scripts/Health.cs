using UnityEngine;

public class Health: MonoBehaviour
{   
   public static Health HealthInstance;
   public int healthAmount = 50;
   public int MaxHealth = 100;

    void Awake()
    {
        if(HealthInstance == null) HealthInstance = this;
        else Destroy(gameObject);
    }

    void Start() 
    {
    
    }

    private void Update() 
    {
        DontDestroyOnLoad(gameObject);
    }
}