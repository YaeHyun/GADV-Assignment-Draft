using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CollectableScript : MonoBehaviour
{

    public string value; // Value of each collectible

    public GameObject interactionText; // Interaction text to collect collectible
    public EquationManager equationManager; // Gets equation manager 
    public TMP_Text valueText;

    private bool playerRange = false;
    public bool collected = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    
    public float floatHeight = 0.1f;
    public float floatSpeed = 2f;
    private Vector3 startPosition;

    public AudioClip collectEffect;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        interactionText.SetActive(false);
        valueText.text = value;

        startPosition = transform.position;
    }

    void Update()
    {
        if (playerRange && !collected && (Keyboard.current.eKey.wasPressedThisFrame))
        {
            Collect();
        }
        FloatAnimation();
    }

    void Collect()
    {
        audioSource.PlayOneShot(collectEffect);
        bool collectAllowed = equationManager.AddValue(value);
        if (collectAllowed)
        {
            collected = true;

            interactionText.SetActive(false);

            Color c = spriteRenderer.color;
            c.a = 0.3f;
            spriteRenderer.color = c;

            col.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            playerRange = true;
            interactionText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerRange = false;
            interactionText.SetActive(false);
        }
    }

    public void ResetCollectible()
    {
        collected = false;

        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;

        col.enabled = true;
    }

    void FloatAnimation()
    {
        float newY = startPosition.y +
                     Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = new Vector3(
            startPosition.x,
            newY,
            startPosition.z
        );
    }

}
