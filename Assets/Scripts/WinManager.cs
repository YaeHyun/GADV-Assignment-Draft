using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class WinManager : MonoBehaviour
{
    public Button mainMenuButton; // Main menu button
    public Image levelUIBackground; // Black background UI
    private float fadeDuration = 1.0f; // Fade duration

    public Sprite[] images; // Images for the cutscene related image (Etc apple pie for level 1)
    public Image displayImage; // Cutscene related image

    public AudioClip winEffect; // Win sound effect
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Gets audio source componenet
        int imageIndex = PlayerPrefs.GetInt("CutsceneImage", 0); // Gets the image index of the cutscene related image
        displayImage.sprite = images[imageIndex]; // Sets the image to the index in the prior
        levelUIBackground.gameObject.SetActive(true); // Sets the black background to true
        StartCoroutine(LevelFadeIn()); // Starts fade in
    }

    public void ExitMainMenu()
    {
        StartCoroutine(LevelFadeExit("Menu")); // Fade out and load main menu scene
    }

    IEnumerator LevelFadeIn() 
    {
        if (levelUIBackground)
        {
            Color backgroundC = levelUIBackground.color; // Gets the color of the UI to change the alpha of it in the code

            float elapsedTime = 0f; // Tracks the time the function runs

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime; // Tracks the time the function runs
                backgroundC.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Lerps the alpha of the background by the elapsed time and fade duration

                levelUIBackground.color = backgroundC;

                yield return null; 
            }

            audioSource.PlayOneShot(winEffect); // Plays winning sound effect
            backgroundC.a = 0f; // Sets the alpha of the background to 0 (Transparent)
            levelUIBackground.color = backgroundC; // Sets the alpha again in case it is broken
        }
    }

    public IEnumerator LevelFadeExit(string level)
    {
        if (levelUIBackground)
        {
            Color backgroundC = levelUIBackground.color; // Gets the color of the UI to change the alpha of it in the code

            float elapsedTime = 0f; // Tracks the time the function runs

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime; // Tracks the time the function runs
                backgroundC.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // Lerps the alpha of the background by the elapsed time and fade duration

                levelUIBackground.color = backgroundC;

                yield return null; 
            }

            backgroundC.a = 1f; // Sets the alpha of the background to 0 (Transparent)
            levelUIBackground.color = backgroundC; // Sets the alpha again in case it is broken
            SceneManager.LoadScene(level); // Loads the main menu scene
        }
    }
}
