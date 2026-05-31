using UnityEngine;

public class Collectables : MonoBehaviour
{
    public static int coins = 0;
    public AudioPlayer audioPlayer;
    public BossMovement bossMovement;
    public float tidePodDriftSpeed = 0.2f;
    private bool tidePodCollected = false;
    private Vector2 tidePodTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private bool initialized = false;

    // Update is called once per frame
    void Update()
    {
        if (!initialized)
        {
            var boss = GameObject.FindWithTag("Boss");
            if (boss != null)
            {
                bossMovement = boss.GetComponent<BossMovement>();
                initialized = true;
            }
        }

        if (tidePodCollected)
        {
            tidePodTarget = bossMovement.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, tidePodTarget, tidePodDriftSpeed * Time.deltaTime);
            if(transform.position == bossMovement.transform.position)
            {
                Debug.Log("TidePod reached the boss! Boss is now vulnerable!");
                Destroy(gameObject);
            }
        }
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
                if (!tidePodCollected)
                {
                    tidePodCollected = true;
                }

                Debug.Log("TidePod collected, boss is now vulnerable!");
            }
        }
    }
}
