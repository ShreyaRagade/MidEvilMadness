<<<<<<< Updated upstream
=======
using System;
using System.Collections;
>>>>>>> Stashed changes
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    public Rigidbody2D rb;
    Animator animator;
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

<<<<<<< Updated upstream

    // Update is called once per frame
=======
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
>>>>>>> Stashed changes
    void Update()
    {
        //Vector2 newVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocityY);
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocityY);
        Gravity();
        animator.SetBool("isJumping", !isGrounded());
        animator.SetFloat("xVel", Math.Abs(rb.linearVelocityX));
        animator.SetFloat("yVel", rb.linearVelocityY);
       
    }
    private void Gravity()
    {
        if (rb.linearVelocityX < 0)
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
    }

    public void Jump(InputAction.CallbackContext context)
    {
<<<<<<< Updated upstream
=======
       
        if (isGrounded())
        {
            if (context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower);
>>>>>>> Stashed changes


        if (context.performed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower);

        }
<<<<<<< Updated upstream


    }

    private bool isGrounded()
    {

        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {

            return true;

        }

        return false;

=======
           
        
>>>>>>> Stashed changes
    }

    private bool isGrounded()
    {

        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {

            Debug.Log("grounded");
            return true;
            

        }

        Debug.Log("Not grounded");
        animator.SetBool("isJumping", !isGrounded());
        return false;


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);
    }
}
