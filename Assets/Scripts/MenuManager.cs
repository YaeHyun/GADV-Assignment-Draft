using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public GameObject levelPanel;

    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button levelReturnButton;
    public Button startButton;
    public Button tutorialButton;
    public Button quitButton;
    public Button resetButton;
    public Image levelUIBackground;
    private float fadeDuration = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LevelFadeIn());
        levelPanel.SetActive(false);

        LoadUnlockedLevels();
        levelUIBackground.gameObject.SetActive(true);
    }

    public void ReturnSelectLevel()
    {
        levelPanel.SetActive(false);
        startButton.gameObject.SetActive(true);
        tutorialButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);
    }

    public void OpenLevelSelect()
    {
        levelPanel.SetActive(true);
        startButton.gameObject.SetActive(false);
        tutorialButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
        LoadUnlockedLevels();
    }

    public void LoadLevel1()
    {
        StartCoroutine(LevelFadeOut("Level1"));  
    }

    public void LoadLevel2()
    {
        StartCoroutine(LevelFadeOut("Level2"));  
    }

    public void LoadLevel3()
    {
        StartCoroutine(LevelFadeOut("Level3"));  
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TutorialSelect()
    {
        StartCoroutine(LevelFadeOut("Tutorial"));  
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);
    }

    void LoadUnlockedLevels()
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        level1Button.interactable = true;
        level2Button.interactable = unlocked >= 2;
        level3Button.interactable = unlocked >= 3;
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

    public IEnumerator LevelFadeOut(string level)
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