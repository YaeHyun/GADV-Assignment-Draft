using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private int health = 3;
    public Button resetButton;
    public Button settingButton;
    public Image heart1;
    public Image heart2;
    public Image heart3;

    public EquationManager equationManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ResetInteraction()
    {
        equationManager.ResetEquation();
    }

    public void DeductHeart()
    {
        StartCoroutine(RemoveHeart());
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

            SceneManager.LoadScene("Menu");


            yield return null;
        }
    }
}
