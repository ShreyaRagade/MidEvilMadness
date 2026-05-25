using UnityEngine;

public class Collectables : MonoBehaviour
{
    public static int coins = 0;
    public AudioPlayer audioPlayer;
    public Bossmovement bossMovement;
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
            if(this.gameObject.CompareTag("Coin"))
            {
                audioPlayer.PlaySoundEffect();
                coins = coins + 1;
                Debug.Log("Coins: " + coins);
                Destroy(gameObject);
            }
             else if(this.gameObject.CompareTag("TidePod"))
            {
                //add state change logic here [1->2, 2->3]
                if(bossMovement.bossState == 1)
                {
                    bossMovement.bossState = 2;
                }
                else if(bossMovement.bossState == 2)
                {
                    bossMovement.bossState = 3;
                }
            }
        }
    }
}
