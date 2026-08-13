using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class WinManager : MonoBehaviour
{
    public Button mainMenuButton;
    public Image levelUIBackground;
    private float fadeDuration = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelUIBackground.gameObject.SetActive(true);
        StartCoroutine(LevelFadeIn());  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitMainMenu()
    {
        StartCoroutine(LevelFadeExit("Menu"));  
    }

    IEnumerator LevelFadeIn()
    {
        if (levelUIBackground)
        {
            Color backgroundC = levelUIBackground.color;

            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                backgroundC.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

                levelUIBackground.color = backgroundC;

                yield return null; 
            }

            backgroundC.a = 0f;
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
