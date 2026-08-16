using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    public float movementSpeed = 5f; // Movement speed variable
    public float jumpForce = 0.01f; // Jump force variable

    private float moveInput = 0f; // Movement input at 1 or -1

    public LayerMask groundLayer; // Ground layer variable
    private Rigidbody2D rb; 
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Gets each of the components in the player sprite
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) // Checks if A or left arrow key is pressed
        {
            moveInput = -1f; // Sets moveinput to -1
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) // Checks if D or right arrow key is pressed
        {
            moveInput = 1f; // Sets moveinput to 1
        }
        else
        {
            moveInput = 0f; // Sets the moveinput to 0 
        }

        rb.linearVelocity = new Vector2(moveInput * movementSpeed, rb.linearVelocity.y); // Changes the velocity based on move input and movement speed by changing the linear velocity of y to the outcome

        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded() || Keyboard.current.upArrowKey.isPressed && IsGrounded() || Keyboard.current.wKey.isPressed && IsGrounded()) // Checks if spacebar, W key or up arrow key is pressed and the player sprite is on the ground
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Sets the linearvelocity of x to the jump force
        }

        Animation();
    }

    private void Animation() // Player Animation Function
    {
        if (moveInput > 0) // Flips the player sprite based on if the moveInput is above or below 0
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        
        if (IsGrounded()) // Checks if player is on the ground
        {
            if(moveInput == 0) // Checks if player is not moving
            {
                animator.Play("AnimationIdle"); // Players idle anim
            }
            else
            {
                animator.Play("AnimationRun"); // Plays running anim
            }
        }
        else
        {
            if (rb.linearVelocity.y > 0) // Checks if the player is going up
            {
                animator.Play("AnimationJump"); // Plays the jumping anim
            }
            else
            {
                animator.Play("AnimationFall"); // Plays falling anim
            }
        }
    }

    private bool IsGrounded() // Checks if the player is on the ground
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast( // Raycast below the player
            capsuleCollider.bounds.center, // Uses the capsule collider but placed right below the player
            capsuleCollider.bounds.size, 
            0f, 
            Vector2.down, 
            0.1f, 
            groundLayer
        );
        
         if (raycastHit.collider == null)
            return false; // If nothing is under the player, it returns false

        return raycastHit.normal.y > 0.7f; // Checks if the surface hit by the player has a y of above 0.7f such that the player is not jumping while colliding with a wall and if not, returns true
    }
}