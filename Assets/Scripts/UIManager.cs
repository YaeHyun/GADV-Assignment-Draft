using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{   
    private int health = 3; // Amount of hearts left
    public Button resetButton; // Reset button
    public Button settingButton; // Return to menu button
    public Image heart1; // Heart 1 - 3
    public Image heart2;
    public Image heart3;

    public Image levelUIBackground; // Black Background UI
    public TMP_Text levelUIText; // Level Number text
    private float fadeDuration = 1.0f; // Fade duration

    public EquationManager equationManager; // Equation Manager

    void Start()
    {
        levelUIBackground.gameObject.SetActive(true); // Sets the black background to true
        StartCoroutine(LevelFadeIn()); // Starts the fade in coroutine
    }

    public void ResetInteraction()
    {
        equationManager.ResetEquation(); // Resets the equation when reset equation button clicked through equation manager function
    }

    public void DeductHeart()
    {
        StartCoroutine(RemoveHeart()); // Deducts heart through coroutine
    }

    public void FadeOut()
    {
        StartCoroutine(LevelFadeOut()); // Level Fade out coroutine
    }

    public void ExitMainMenu()
    {
        StartCoroutine(LevelFadeExit("Menu")); // Exit to main menu
    }

    IEnumerator RemoveHeart()
    {
        if (health == 3) // Checks the amount of hearts
        {
            Color c = heart3.color; // Gets color of targetted heart

            for (int i = 0; i < 3; i++) // iterates 3 times
            {
                c.a = 0.3f; // Sets heart alpha to 0.3
                heart3.color = c; // Sets heart translucent
                yield return new WaitForSeconds(0.3f); // Waits 0.3 seconds

                c.a = 1f; // Sets heart alpha to 1
                heart3.color = c;
                yield return new WaitForSeconds(0.3f);
            }

            c.a = 0.3f; // Sets heart alpha to 0.3 
            heart3.color = c;

            health -= 1; // Remove the overall heart

            yield return null;
        }
        else if (health == 2) // Repeats as above
        {
            Color c = heart2.color;

            for (int i = 0; i < 3; i++)
            {
                c.a = 0.3f;
                heart2.color = c;
                yield return new WaitForSeconds(0.3f);

                c.a = 1f;
                heart2.color = c;
                yield return new WaitForSeconds(0.3f);
            }

            c.a = 0.3f;
            heart2.color = c;

            health -= 1;

            yield return null;
        }
        else
        {
            Color c = heart1.color; // Repeats as above

            for (int i = 0; i < 3; i++)
            {
                c.a = 0.3f;
                heart1.color = c;
                yield return new WaitForSeconds(0.3f);

                c.a = 1f;
                heart1.color = c;
                yield return new WaitForSeconds(0.3f);
            }

            c.a = 0.3f;
            heart1.color = c;

            yield return new WaitForSeconds(2f);
            StartCoroutine(LevelFadeOut()); // Level fades out
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("Menu"); // Loads main menu scene


            yield return null;
        }
    }


    IEnumerator LevelFadeIn()
    {
        if (levelUIBackground && levelUIText)
        {
            yield return new WaitForSeconds(2f);

            Color backgroundC = levelUIBackground.color; // Gets the color of the background UI to change the alpha of it in the code
            Color textC = levelUIText.color; // Gets the color of the text UI to change the alpha of it in the code

            float elapsedTime = 0f; // Tracks the time the function runs

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime; // Tracks the time the function runs
                backgroundC.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Lerps the alpha of the background by the elapsed time and fade duration
                textC.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Lerps the alpha of the text by the elapsed time and fade duration

                levelUIBackground.color = backgroundC;
                levelUIText.color = textC;

                yield return null; 
            }

            backgroundC.a = 0f;
            textC.a = 0f;
            levelUIBackground.color = backgroundC; // Sets the alpha of the background to 0 (Transparent)
            levelUIText.color = textC; // Sets the alpha of the text to 0 (Transparent)
        }
    }

    public IEnumerator LevelFadeOut()
    {
        if (levelUIBackground) // Same as above
        {
            Color backgroundC = levelUIBackground.color;

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
        }
    }

    public IEnumerator LevelFadeExit(string level)
    {
        if (levelUIBackground) // Same as above
        {
            Color backgroundC = levelUIBackground.color;

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
            SceneManager.LoadScene(level); // Loads scene based on string given through function
        }
    }
}
