using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CollectableScript : MonoBehaviour
{

    public string value; // Value of each collectible

    public GameObject interactionText; // Interaction text to collect collectible
    public EquationManager equationManager; // Gets equation manager 
    public TMP_Text valueText; // Gets the value TMP 

    private bool playerRange = false; // Player in range 
    public bool collected = false; // Collectable collected

    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    
    public float floatHeight = 0.1f; // Max height to float at
    public float floatSpeed = 2f; // Speed to float at
    private Vector3 startPosition; // Starting position of collectable

    public AudioClip collectEffect; // Collect sound effect
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Gets components in collectable
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        interactionText.SetActive(false); // Sets interaction text to false
        valueText.text = value; // Sets value tmp to value assigned

        startPosition = transform.position; // Sets starting postiion to position started at
    }

    void Update()
    {
        if (playerRange && !collected && (Keyboard.current.eKey.wasPressedThisFrame)) // Checks if the player is in range, isnt collected already and the player pressed e
        {
            Collect(); // Collects the collectable
        }
        FloatAnimation(); // Always plays the floating animation
    }

    void Collect()
    {
        audioSource.PlayOneShot(collectEffect); // Plays collect sound effect
        bool collectAllowed = equationManager.AddValue(value); // Sends the value to the equation manager and stores the boolean value returned
        if (collectAllowed)
        {
            collected = true; // If collected, enable it to be collected so it cannot be collected again

            interactionText.SetActive(false); // Disables interaction text

            Color c = spriteRenderer.color; // Gets the color to change the alpha of the collectable
            c.a = 0.3f; // Changes the alpha to 0.3 so its translucent
            spriteRenderer.color = c; // Changes the alpha

            col.enabled = false; // Disables the collider
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !collected) // Checks if the item triggering the collider is a player and if the collectable is not already collected
        {
            playerRange = true; // Sets player in range to true
            interactionText.SetActive(true); // Sets interaction text to true
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Checks if the item triggering the collider is a player and if the collectable is not already collected
        {
            playerRange = false; // Sets player in range to false
            interactionText.SetActive(false); // Sets interaction text to false
        }
    }

    public void ResetCollectible() // Resets the collectable so it can be collected again
    {
        collected = false; // Sets collected to false

        Color c = spriteRenderer.color; // Resets the alpha of the collectable
        c.a = 1f;
        spriteRenderer.color = c;

        col.enabled = true; // Enables the collider
    }

    void FloatAnimation() // Floating animation of the collectable 
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight; // Float animation using sin function
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

}
