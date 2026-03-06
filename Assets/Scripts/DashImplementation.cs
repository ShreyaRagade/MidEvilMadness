/*using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashImplementation : MonoBehaviour
{
    public Rigidbody2D rb;
    bool isFacingRight = true;
    public ParticleSystem smokeFX;


    [Header("Dashing")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 0.1f;
    bool isDashing;
    bool canDash = true;
    TrailRenderer trailRenderer;
   

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);
    public LayerMask groundLayer;
    bool isGrounded;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallGravityMult = 2f;
    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }


    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }
    private IEnumerator DashCoroutine()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        canDash = false;
        isDashing = true;
        trailRenderer.emitting = true;

        float dashDirection = isFacingRight ? 1f : -1f;

        rb.linearVelocity = new Vector2(dashDirection * dashSpeed, rb.linearVelocityY); //Dash movement

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = new Vector2(0f, rb.linearVelocityY); //Reset horizontal velocity

        isDashing = false;
        trailRenderer.emitting = false;
        Physics2D.IgnoreLayerCollision(7, 8, false);

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

   

  
    private void OnDrawGizmosSelected()
    {
        //Ground check visual
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        Gizmos.color = Color.blue;
      //  Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    }
} */
