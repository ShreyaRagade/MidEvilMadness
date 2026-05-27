using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour
{
    public EmemyMovement enemymovement; 
    public DrierMovement driermovement; 
    private int bossState = 1;
    private float movementSpeed = 2.5f;
    public GameObject Player;
    public GameObject TidePod;
    private GameObject currentTidePod = null;
    


    private Vector2 movementDirection;
    public Vector2 stopAtPosition = new Vector2(0, -10.95372f);
    private bool stage1 = true;
    private bool stage2 = false;
    private bool stage3 = false;
    private int jumpNum = 0;
    private int stage1BounceCounter = 0;
    private bool bossJumping = false;

    private Rigidbody2D bossRb;
    private Transform bossTransform;

    public GameObject EnemyBOSS;
    public GameObject DrierEnemyBOSS;
    public GameObject AirRaidMissile;
    public GameObject Spike;

    private Coroutine airRFunction;

    private int xDDir = -1; // -1 = left, 1 = right
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossTransform = transform.parent;
        bossRb = bossTransform.GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        enemymovement = GameObject.FindWithTag("Enemy").GetComponent<EmemyMovement>();
        driermovement = GameObject.FindWithTag("DrierEnemy").GetComponent<DrierMovement>();

        EnemyBOSS = GameObject.FindWithTag("Enemy").transform.parent.gameObject;
        DrierEnemyBOSS = GameObject.FindWithTag("DrierEnemy").transform.parent.gameObject;
        TidePod = GameObject.FindWithTag("TidePod");
        AirRaidMissile = GameObject.FindWithTag("missile");
    }


    // Update is called once per frame
    void Update()
    {    
        if(stage1 == true && bossState == 1)
        {
            bossRb.linearVelocity = new Vector2(xDDir * movementSpeed, bossRb.linearVelocity.y);
        }
        if((stage2 == false && bossState == 2) || (stage3 == false && bossState == 3))
        {
            bossRb.linearVelocity = new Vector2(0,0);

            float distance = Vector2.Distance(transform.position, stopAtPosition);

            if (distance > 0.1f) {
                bossTransform.position = Vector2.MoveTowards(transform.position, stopAtPosition, 2f * Time.deltaTime);
            }
            else if(stage2 == false){
                bossTransform.position = stopAtPosition;
                stage2 = true;
                InvokeRepeating("bossJump", 3.0f, 5.0f);
            }
            else{
                jumpNum = 0;
                bossTransform.position = stopAtPosition;
                stage3 = true;
                InvokeRepeating("bossJump2", 3.0f, 10.0f);
                
            }
        }
    }

    void bossJump()
    {
        if(jumpNum == 0)
        {
            bossRb.linearVelocity = new Vector2(Random.Range(-10, -5), Random.Range(10, 15));
            jumpNum += 1;
        }
        else if(jumpNum == 1)
        {
            bossRb.linearVelocity = new Vector2(Random.Range(10, 12), Random.Range(16, 20));
            jumpNum += 1;
            spawnTidPod();
        }
        else if(jumpNum == 2)
        {
            bossRb.linearVelocity = new Vector2(Random.Range(-12, -10), Random.Range(16, 20));
            jumpNum += 1;
        }
        else if(jumpNum == 3)
        {
            bossRb.linearVelocity = new Vector2(Random.Range(-10, -5), Random.Range(10, 20));
            jumpNum += 1;
            CancelInvoke("bossJump");
            Debug.Log("NOW STAGE 3");
            bossState = 3;
            if(currentTidePod != null)
            {
                Destroy(currentTidePod);
                currentTidePod = null;
            }
        }
        bossJumping = true;
    }

    void bossJump2()
    {
        if(jumpNum % 3 == 0 || jumpNum % 3 == 1)
        {        
            bossRb.linearVelocity = new Vector2(Random.Range(-6, 6), Random.Range(15, 20));
        }
        else if(jumpNum % 3 == 1)
        {
            bossRb.linearVelocity = new Vector2(Random.Range(-6, 6), Random.Range(15, 20));
        }
        else
        {
            doAirRaid();
            if(currentTidePod != null)
            {
                Destroy(currentTidePod);
                currentTidePod = null;
            }
            spawnTidPod();
        }
        jumpNum += 1;

        bossJumping = true;
    }

    void spawnRandEnemy()
    {
        float random = UnityEngine.Random.value;
        random = 1;
        Vector3 leftV = new Vector3(10, 0, 0);

        int targetDir = 1; //right
        if (Player != null && Player.transform.position.x < bossTransform.position.x)
        {
          leftV.x *= -1;
          targetDir = -1; //left
        }
        GameObject clone;
        if(random > 0.5)
        {
            clone = Instantiate(EnemyBOSS, bossTransform.position + leftV , transform.rotation);
        }
        else
        {
           clone = Instantiate(DrierEnemyBOSS, bossTransform.position + leftV, transform.rotation);
        }
        
        if(random > 0.5)
        {
            EmemyMovement cloneMovement = clone.GetComponentInChildren<EmemyMovement>();

            cloneMovement.xEDir = targetDir;
        }
        else
        {
            DrierMovement cloneMovement = clone.GetComponentInChildren<DrierMovement>();
            cloneMovement.xDDir = targetDir;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {       
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("BossCollider"))
        {  
            if(bossState == 1 && other.gameObject.CompareTag("BossCollider"))
            {  
                xDDir *= -1;
                spawnRandEnemy();
                stage1BounceCounter += 1;

                if(stage1BounceCounter % 3 == 0)
                {
                    spawnTidPod();
                }
            }
        }
        if (other.gameObject.CompareTag("HitTidePod"))
        {
            if(bossState == 1)
            {
                bossState = 2;
                Debug.Log("NOW STAGE 2");
            }
            else if(bossState == 2)
            {
                bossState = 3;
                CancelInvoke("bossJump");
                Debug.Log("NOW STAGE 3");
            }
            else if(bossState == 3)
            {
                CancelInvoke("bossJump2");
                Debug.Log("Killed Boss");
                //delete boss, then timer 5 seconds, then go to main menu
            }
            Destroy(currentTidePod);
            currentTidePod = null;
        }
        if(other.gameObject.CompareTag("Floor") && bossJumping == true)
        {
            //Debug.Log("SPAWN");
            if(bossState == 2)
            {
                spawnRandEnemy();
            }
            else if(bossState == 3)
            {
                spawnRandEnemy();
                spikeWave();
            }
            bossJumping = false;

        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Player"))
        {
            //kill player
        }
    }

    void spawnTidPod()
    {
        if(currentTidePod != null)
        {
            Destroy(currentTidePod);
            currentTidePod = null;
        }
        float randx = (UnityEngine.Random.value * 9) + 9;
        if(UnityEngine.Random.value < 0.5)
        {
            randx *= -1;
        }
        Vector3 TidePodSpawnVector = new Vector3(randx, -15.5f, 0);
        currentTidePod = Instantiate(TidePod, TidePodSpawnVector, transform.rotation);
    }
    
    void doAirRaid() 
    {         
        int targetDir = 1; //right
        if (Player != null && Player.transform.position.x < bossTransform.position.x)
        {
          targetDir = -1; //left
        }
        airRFunction = StartCoroutine(doAirRaidHelper(0.2f, targetDir)); 
    }

    IEnumerator doAirRaidHelper(float delay, int side)
    {
        float speed = 20f;

        for (int angle = 85; angle >= 70; angle -= 4)
        {
            float radians = angle * Mathf.Deg2Rad;

            Vector2 newVelocity = new Vector2(side * Mathf.Cos(radians), Mathf.Sin(radians)) * speed;

            GameObject missileClone = Instantiate(AirRaidMissile, bossTransform.position + new Vector3(0, 7, 0), transform.rotation);

            Rigidbody2D missileRb = missileClone.GetComponentInChildren<Rigidbody2D>();

            missileRb.linearVelocity = newVelocity;

            yield return new WaitForSeconds(delay);
        }
    }
    void spikeWave()
    {
        GameObject spikeLeft;
        GameObject spikeRight;
        //spawn 2 spikes on the left and 2 spikes on the right of the boss
        spikeLeft = Instantiate(Spike, transform.parent.position - new Vector3(0, 5.5f, 0), transform.rotation);
        spikeRight = Instantiate(Spike, transform.parent.position - new Vector3(0, 5.5f, 0), transform.rotation);
        spikeRight.tag = "rightSpikeWave";
        spikeLeft.tag = "leftSpikeWave";
    }

    


}
//-10.95372


