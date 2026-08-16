using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public Image cutscene1; // Cutscene images
    public Image cutscene2;
    public Image cutscene3;
    public Image levelUIBackground; // Black background UI to fade in & out
    private float fadeDuration = 1.0f; // Duration of fading

    void Start()
    {
        levelUIBackground.gameObject.SetActive(true); // Sets the black background to true and only enables the correct cutscene to show
        cutscene1.gameObject.SetActive(true);
        cutscene2.gameObject.SetActive(false);
        cutscene3.gameObject.SetActive(false);   

        StartCoroutine(Cutscene()); // Starts the coroutine to show all three cutscenes
    }

    IEnumerator FadeIn()
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

            backgroundC.a = 0f; // Sets the alpha of the background to 0 (Transparent)
            levelUIBackground.color = backgroundC; // Sets the alpha again in case it is broken
        }
    }

    IEnumerator Cutscene() // Hardcoded instead of using a iteration code due to being implemented last-minute
    {
        yield return new WaitForSeconds(1f); // Each of the lines fades in and out while disabling and enabling the correct cutscene images
        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(FadeOut());
        yield return new WaitForSeconds(1f);
        cutscene1.gameObject.SetActive(false);
        cutscene2.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(FadeOut());
        yield return new WaitForSeconds(1f);
        cutscene2.gameObject.SetActive(false);
        cutscene3.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(FadeOut());
        yield return new WaitForSeconds(1f); 

        string currentScene = SceneManager.GetActiveScene().name; // Finds the name of the scene to properly load the next correct scene
        if (currentScene == "Cutscene1")
        {
            SceneManager.LoadScene("Level1");    
        }
        else if (currentScene == "Cutscene2")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (currentScene == "Cutscene3")
        {
            SceneManager.LoadScene("Level3");
        }
    }

    public IEnumerator FadeOut()
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
        }
    }
}
