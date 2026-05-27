using UnityEngine;


public class BossMovement : MonoBehaviour
{
    public EmemyMovement enemymovement; 
    public DrierMovement driermovement; 
    private int bossState = 1;
    private float movementSpeed = 2f;
    public GameObject Player;


    private Vector2 movementDirection;
    public Vector2 stopAtPosition = new Vector2(0, -10.95372f);
    private bool stage1 = true;
    private bool stage2 = false;
    private bool stage3 = false;
    private int jumpNum = 0;
    private bool bossJumping = false;

    private Rigidbody2D parentRb;

    public GameObject EnemyBOSS;
    public GameObject DrierEnemyBOSS;

    private int xDDir = -1; // -1 = left, 1 = right
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentRb = transform.parent.GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        enemymovement = GameObject.FindWithTag("Enemy").GetComponent<EmemyMovement>();
        driermovement = GameObject.FindWithTag("DrierEnemy").GetComponent<DrierMovement>();

        EnemyBOSS = GameObject.FindWithTag("Enemy").transform.parent.gameObject;
        DrierEnemyBOSS = GameObject.FindWithTag("DrierEnemy").transform.parent.gameObject;
        
    }


    // Update is called once per frame
    void Update()
    {    
        if(stage1 == true && bossState == 1)
        {
            parentRb.linearVelocity = new Vector2(xDDir * movementSpeed, parentRb.linearVelocity.y);
        }
        if((stage2 == false && bossState == 2) || (stage3 == false && bossState == 3))
        {
            parentRb.linearVelocity = new Vector2(0,0);

            float distance = Vector2.Distance(transform.position, stopAtPosition);

            if (distance > 0.1f) {
                transform.parent.position = Vector2.MoveTowards(transform.position, stopAtPosition, 2f * Time.deltaTime);
            }
            else if(stage2 == false){
                transform.parent.position = stopAtPosition;
                stage2 = true;
                InvokeRepeating("bossJump", 3.0f, 5.0f);
            }
            else{
                transform.parent.position = stopAtPosition;
                stage3 = true;
                InvokeRepeating("bossJump2", 3.0f, 10.0f);
                jumpNum = 0;
            }
        }
    }

    void bossJump()
    {
        if(jumpNum == 0)
        {
            parentRb.linearVelocity = new Vector2(Random.Range(-10, -5), Random.Range(10, 15));
            jumpNum += 1;
        }
        else if(jumpNum == 1)
        {
            parentRb.linearVelocity = new Vector2(Random.Range(10, 12), Random.Range(16, 20));
            jumpNum += 1;
        }
        else if(jumpNum == 2)
        {
            parentRb.linearVelocity = new Vector2(Random.Range(-12, -10), Random.Range(16, 20));
            jumpNum += 1;
        }
        else if(jumpNum == 3)
        {
            parentRb.linearVelocity = new Vector2(Random.Range(-10, -5), Random.Range(10, 20));
            jumpNum += 1;
            CancelInvoke("bossJump");
            bossState = 3;
        }
    }


    void bossJump2()
    {
        jumpNum += 1;
        if(jumpNum % 3 == 0)
        {        
            parentRb.linearVelocity = new Vector2(Random.Range(-4, 4), Random.Range(15, 20));
            //Spawn Enemy
        }
        else if(jumpNum % 3 == 1)
        {
            parentRb.linearVelocity = new Vector2(Random.Range(-4, 4), Random.Range(15, 20));
            //Spike Wave
        }
        else
        {
            //AirRaid
        }
    }


    void spawnRandEnemy()
    {
        float random = UnityEngine.Random.value;
        random = 1;
        Vector3 leftV = new Vector3(10, 0, 0);

        int targetDir = 1; //right
        if (Player != null && Player.transform.position.x < transform.parent.position.x)
        {
          leftV.x *= -1;
          targetDir = -1; //left
        }
        GameObject clone;
        if(random > 0.5)
        {
            clone = Instantiate(EnemyBOSS, transform.parent.position + leftV , transform.rotation);
        }
        else
        {
           clone = Instantiate(DrierEnemyBOSS, transform.parent.position + leftV, transform.rotation);
        }
        /*Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0;
        }*/

        
        if(random > 0.5)
        {
            EmemyMovement cloneMovement = clone.GetComponentInChildren<EmemyMovement>();

            cloneMovement.xEDir = targetDir;
        }
        else{
            DrierMovement cloneMovement = clone.GetComponentInChildren<DrierMovement>();

            cloneMovement.xDDir = targetDir;
        }
        
        //clone.transform.position += new Vector3(0, 10, 0);

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered has a specific tag
       
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("BossCollider"))
        {  
            if(bossState == 1)
            {  
                xDDir *= -1;
                spawnRandEnemy();
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            bossState = 2;
        }
    }


}
//-10.95372
    //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


