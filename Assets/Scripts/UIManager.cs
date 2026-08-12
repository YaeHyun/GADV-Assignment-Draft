using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{   
    private int health = 3;
    public Button resetButton;
    public Button settingButton;
    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Image levelUIBackground;
    public TMP_Text levelUIText;
    private float fadeDuration = 1.0f;

    public EquationManager equationManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LevelFadeIn());   
    }

    public void ResetInteraction()
    {
        equationManager.ResetEquation();
    }

    public void DeductHeart()
    {
        StartCoroutine(RemoveHeart());
    }

    public void FadeOut()
    {
        StartCoroutine(LevelFadeOut());
    }

    public void ExitMainMenu()
    {
        StartCoroutine(LevelFadeExit("Menu"));  
    }

    IEnumerator RemoveHeart()
    {
        if (health == 3)
        {
            Color c = heart3.color;

            for (int i = 0; i < 3; i++)
            {
                c.a = 0.3f;
                heart3.color = c;
                yield return new WaitForSeconds(0.3f);

                c.a = 1f;
                heart3.color = c;
                yield return new WaitForSeconds(0.3f);
            }

            c.a = 0.3f;
            heart3.color = c;

            health -= 1;

            yield return null;
        }
        else if (health == 2)
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
            Color c = heart1.color;

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
            StartCoroutine(LevelFadeOut()); 
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("Menu");


            yield return null;
        }
    }


    IEnumerator LevelFadeIn()
    {
        if (levelUIBackground && levelUIText)
        {
            yield return new WaitForSeconds(2f);

            Color backgroundC = levelUIBackground.color;
            Color textC = levelUIText.color;

            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                backgroundC.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                textC.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

                levelUIBackground.color = backgroundC;
                levelUIText.color = textC;

                yield return null; 
            }

            backgroundC.a = 0f;
            textC.a = 0f;
            levelUIBackground.color = backgroundC;
            levelUIText.color = textC;
        }
    }

    public IEnumerator LevelFadeOut()
    {
        if (levelUIBackground)
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
        if (levelUIBackground)
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
            SceneManager.LoadScene(level);
        }
    }
}
