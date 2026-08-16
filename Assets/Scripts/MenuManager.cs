using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public GameObject levelPanel; // Gets levels UI container

    public Button level1Button; // Gets all UI elements
    public Button level2Button;
    public Button level3Button;
    public Button levelReturnButton;
    public Button startButton;
    public Button tutorialButton;
    public Button quitButton;
    public Button resetButton;
    public Image levelUIBackground; // Gets UI background
    private float fadeDuration = 1.0f; // Fade duration

    public AudioClip clickEffect; // Click sound effect
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get audio source component
        StartCoroutine(LevelFadeIn()); // Fade in
        levelPanel.SetActive(false); // Sets levels UI container to false

        LoadUnlockedLevels(); // Loads the levels unlocked
        levelUIBackground.gameObject.SetActive(true); // Sets the background to true
    }

    public void ReturnSelectLevel()
    {
        PlaySound();
        levelPanel.SetActive(false); // Sets level panel to false
        startButton.gameObject.SetActive(true); // Sets the other UI elements prior to true
        tutorialButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);
    }

    public void OpenLevelSelect()
    {
        PlaySound();
        levelPanel.SetActive(true); // Sets level panel to true
        startButton.gameObject.SetActive(false); // Sets the other UI elements prior to false
        tutorialButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
        LoadUnlockedLevels(); // Loads levels unlocked
    }

    public void LoadLevel1()
    {
        PlaySound();
        StartCoroutine(LevelFadeOut("Cutscene1")); // Places and loads cutscene based on level selected
    }

    public void LoadLevel2()
    {
        PlaySound();
        StartCoroutine(LevelFadeOut("Cutscene2"));  
    }

    public void LoadLevel3()
    {
        PlaySound();
        StartCoroutine(LevelFadeOut("Cutscene3"));  
    }

    public void QuitGame()
    {
        PlaySound();
        Application.Quit(); // Quits game
    }

    public void TutorialSelect()
    {
        PlaySound();
        StartCoroutine(LevelFadeOut("Tutorial")); // Loads tutorial scene
    }

    public void ResetProgress()
    {
        PlaySound();
        PlayerPrefs.SetInt("UnlockedLevel", 1); // Resets level progress
    }

    void LoadUnlockedLevels()
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1); // Loads the levels unlocked by player

        level1Button.interactable = true; // Sets level button interactable based on levels unlocked by player
        level2Button.interactable = unlocked >= 2;
        level3Button.interactable = unlocked >= 3;
    }

    IEnumerator LevelFadeIn()
    {
        if (levelUIBackground)
        {
            Color backgroundC = levelUIBackground.color;  // Gets the color of the background UI to change the alpha of it in the code

            float elapsedTime = 0f; // Tracks the time the function runs

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime; // Tracks the time the function runs
                backgroundC.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

                levelUIBackground.color = backgroundC;

                yield return null; 
            }

            backgroundC.a = 0f;
            levelUIBackground.color = backgroundC; // Sets the alpha of the background to 0 (Transparent)
        }
    }

    public IEnumerator LevelFadeOut(string level)
    {
        if (levelUIBackground)
        {
            Color backgroundC = levelUIBackground.color; // Same as above

            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                backgroundC.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

                levelUIBackground.color = backgroundC;

                yield return null; 
            }

            backgroundC.a = 1f;
            levelUIBackground.color = backgroundC;
            SceneManager.LoadScene(level); // Load level based on level given in function
        }
    }

    void PlaySound()
    {
            audioSource.PlayOneShot(clickEffect); // Plays the click sound effect
    }
}