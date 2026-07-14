using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CollectableScript : MonoBehaviour
{

    public string value;

    public GameObject interactionText;
    public EquationManager equationManager;
    public TMP_Text valueText;

    private bool playerRange = false;
    public bool collected = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        interactionText.SetActive(false);
        valueText.text = value;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRange && !collected && (Keyboard.current.eKey.wasPressedThisFrame))
        {
            Collect();
        }
    }

    void Collect()
    {

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
}
