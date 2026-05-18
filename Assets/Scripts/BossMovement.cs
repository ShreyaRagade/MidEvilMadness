using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private int bossState = 1;
    private float movementSpeed = 2f;

    private Vector2 movementDirection;
    private Vector2 stopAtPosition = new Vector2(0, -10.95372f);
    private bool stage1 = true;
    private bool stage2 = false;
    private bool stage3 = false;
    private int jumpNum = 0;

    private Rigidbody2D parentRb;

    private int xDir = -1; // -1 = left, 1 = right
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentRb = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {    
        if(stage1 == true && bossState == 1)
        {
            parentRb.linearVelocity = new Vector2(xDir * movementSpeed, parentRb.linearVelocity.y);
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

    void bossJump(){
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

    void bossJump2(){
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
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered has a specific tag
        
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("BossCollider"))
        {   
            if(bossState == 1)
            {  
                xDir *= -1;
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
