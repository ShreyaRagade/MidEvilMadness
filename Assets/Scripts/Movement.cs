using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    public Rigidbody2D rb;
    Animator animator;
    bool isFacingRight = true;
    public bool isMoving = false;

    [Header("Movement")]
    public float moveSpeed = 5f;

    float horizontalMovement;

    [Header("Jump")]
    public float jumpPower = 10f;
    public int maxJumps = 1;


    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f); //default values 
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 1f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
    }
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        Flip();
        bool grounded = isGrounded();

        if(horizontalMovement == 0)
        {
            isMoving = false;
        }
        
        Gravity();

        
       // Debug.Log(grounded);

        animator.SetFloat("xVel", Mathf.Abs(rb.linearVelocity.x));
       
        if (!grounded)
        {
            animator.SetFloat("yVel", rb.linearVelocity.y);
        }
    }
    private void Gravity()
    {
        if (rb.linearVelocityY < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {

        horizontalMovement = context.ReadValue<Vector2>().x;
        isMoving = true;
        
    }

    public void Jump(InputAction.CallbackContext context)
    {

       
            if (context.performed && isGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower);
               // animator.SetBool("isJumping", !isGrounded());

            }
        



    }

    private bool isGrounded()
    {
        bool grounded = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);

        animator.SetBool("isJumping", !grounded);

       // snap yVel to 0 immediately on landing
        if (grounded)
        {
            animator.SetFloat("yVel", 0f);
        }

        return grounded;
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);
    }
}
