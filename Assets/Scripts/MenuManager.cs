using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelPanel.SetActive(false);

        LoadUnlockedLevels();
    }

    public void ReturnSelectLevel()
    {
        levelPanel.SetActive(false);
        startButton.gameObject.SetActive(true);
        tutorialButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    public void OpenLevelSelect()
    {
        levelPanel.SetActive(true);
        startButton.gameObject.SetActive(false);
        tutorialButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void QuitGame()
    {

    }

    public void TutorialSelect()
    {

    }

    void LoadUnlockedLevels()
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        level1Button.interactable = true;
        level2Button.interactable = unlocked >= 2;
        level3Button.interactable = unlocked >= 3;
    }
}