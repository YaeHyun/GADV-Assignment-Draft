using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 0.01f;

    private float moveInput = 0f;

    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer spriteRenderer;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput = 1f;
        }
        else
        {
            moveInput = 0f;
        }

        rb.linearVelocity = new Vector2(moveInput * movementSpeed, rb.linearVelocity.y);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded() || Keyboard.current.upArrowKey.isPressed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        Animation();
    }

    private void Animation()
    {
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        
        if (IsGrounded())
        {
            if(moveInput == 0)
            {
                animator.Play("AnimationIdle");
            }
            else
            {
                animator.Play("AnimationRun");
            }
        }
        else
        {
            if (rb.linearVelocity.y > 0)
            {
                animator.Play("AnimationJump");
            }
            else
            {
                animator.Play("AnimationFall");
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            capsuleCollider.bounds.center, 
            capsuleCollider.bounds.size, 
            0f, 
            Vector2.down, 
            0.1f, 
            groundLayer
        );
        return raycastHit.collider != null;
    }
}