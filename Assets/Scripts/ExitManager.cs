using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class ExitManager : MonoBehaviour
{
    public int value = 20;
    public int level = 0;

    public GameObject interactionText;
    public TMP_Text valueText;
    public EquationManager equationManager;
    public UIManager uiManager;
    public GameObject iconCollectable;
    public GameObject iconResultTrue;
    public GameObject iconResultFalse;

    private bool cooldown = false;
    private bool playerRange = false;

    private Collider2D col;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();

        interactionText.SetActive(false);
        iconResultTrue.SetActive(false);
        iconResultFalse.SetActive(false);
        valueText.text = value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRange && (Keyboard.current.eKey.wasPressedThisFrame) && !cooldown)
        {
            SubmitEquation();

            cooldown = true;
        }
    }

    void SubmitEquation()
    {
        bool equationAllowed = equationManager.EquationCheck();

        if (equationAllowed)
        {
            StartCoroutine(PassedRoutine());
        }
        else
        {
            StartCoroutine(FailedRoutine());
        }
    }

    IEnumerator PassedRoutine()
    {
        iconCollectable.SetActive(false);
        valueText.enabled = false;
        iconResultTrue.SetActive(true);
        Debug.Log("Passed");

        yield return new WaitForSeconds(1f);
        uiManager.FadeOut();
        yield return new WaitForSeconds(2f);

        int current = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (current < 2)
        {
            PlayerPrefs.SetInt("UnlockedLevel", 2);
        }
        else if (current < 3 && level == 2)
        {
            PlayerPrefs.SetInt("UnlockedLevel", 3);
        }

        SceneManager.LoadScene("Menu");
    }

    IEnumerator FailedRoutine()
    {
        iconCollectable.SetActive(false);
        valueText.enabled = false;
        iconResultFalse.SetActive(true);
        Debug.Log("Failed");
        equationManager.ResetEquation();
        uiManager.DeductHeart();


        yield return new WaitForSeconds(3f);

        iconCollectable.SetActive(true);
        valueText.enabled = true;
        iconResultFalse.SetActive(false);
        cooldown = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
}
