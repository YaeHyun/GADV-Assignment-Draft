using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class ExitManager : MonoBehaviour
{
    public int value = 20; // Target value
    public int level = 0; // Gets level

    public GameObject interactionText; // Gets UI, TMP and Game objects in exit manager
    public TMP_Text valueText;
    public EquationManager equationManager; // Gets equation manager
    public UIManager uiManager; // Gets ui manager
    public GameObject iconCollectable;
    public GameObject iconResultTrue;
    public GameObject iconResultFalse;

    private bool cooldown = false;
    private bool playerRange = false;

    public AudioClip loseEffect; // Lose sound effect
    private AudioSource audioSource;

    private Collider2D col; // Gets collider for interaction area 
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Gets components in the exit manager game object
        col = GetComponent<Collider2D>();

        interactionText.SetActive(false); // Presets all the gameobjects/ui to show properly
        iconResultTrue.SetActive(false);
        iconResultFalse.SetActive(false);
        valueText.text = value.ToString();
    }

    void Update()
    {
        if (playerRange && (Keyboard.current.eKey.wasPressedThisFrame) && !cooldown) // Checks if player presses E and interacts with exit
        {
            SubmitEquation(); // Submits equation to check if its correct

            cooldown = true;
        }
    }

    void SubmitEquation() // Checks if equation is correct or wrong
    {
        bool equationAllowed = equationManager.EquationCheck(); // Stores returned boolean value from equationManager script function 

        if (equationAllowed) // Starts corotine based on if equation is passed or failed
        {
            StartCoroutine(PassedRoutine()); 
        }
        else
        {
            StartCoroutine(FailedRoutine());
        }
    }

    IEnumerator PassedRoutine() // If the equation is correct, the player exits level
    {
        iconCollectable.SetActive(false); // Disables collectible icon and text
        valueText.enabled = false;
        iconResultTrue.SetActive(true); // Enables tick icon
        Debug.Log("Passed");

        yield return new WaitForSeconds(1f);
        uiManager.FadeOut(); // Fades screen out
        yield return new WaitForSeconds(1f);

        int current = PlayerPrefs.GetInt("UnlockedLevel", 1); // Gets the players maximum unlocked level
        string currentScene = SceneManager.GetActiveScene().name; // Gets the name of the current scene

        if (current < 2)
        {
            PlayerPrefs.SetInt("UnlockedLevel", 2); // Sets the maximum unlocked level based on current level and unlocked level
        }
        else if (current < 3 && level == 2)
        {
            PlayerPrefs.SetInt("UnlockedLevel", 3);
        }

        if (currentScene == "Level1")
        {
            PlayerPrefs.SetInt("CutsceneImage", 0); // Sets the winning scene image to dynamically change when loading the scene
        }
        else if (currentScene == "Level2")
        {
            PlayerPrefs.SetInt("CutsceneImage", 1);
        }
        else if (currentScene == "Level3")
        {
            PlayerPrefs.SetInt("CutsceneImage", 2);
        }

        SceneManager.LoadScene("WinScene"); // Loads winning scene
    }

    IEnumerator FailedRoutine() // If the equation is wrong, player loses heart
    {
        iconCollectable.SetActive(false); //Disables icon and text of collectible
        valueText.enabled = false;
        iconResultFalse.SetActive(true); // Enables the red cross
        Debug.Log("Failed");
        audioSource.PlayOneShot(loseEffect); // Plays lose sound effect
        equationManager.ResetEquation(); // Resets the equation by using the reset equation function in equation manager script
        uiManager.DeductHeart(); // Deducts a heart 


        yield return new WaitForSeconds(3f); // Waits 3 seconds

        iconCollectable.SetActive(true); // Resets the icon and value needed
        valueText.enabled = true;
        iconResultFalse.SetActive(false); // Disables the red cross
        cooldown = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Checks if the part that triggered the collider is a player
        {
            playerRange = true; // Enables the player in range variable
            interactionText.SetActive(true); // Enables the interaction text 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Checks if the part that left the collider is a player
        {
            playerRange = false; // Disables player in range variable
            interactionText.SetActive(false); // Disables the interaction text
        }
    }
}
