using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button resetButton;
    public Button settingButton;

    public EquationManager equationManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ResetInteraction()
    {
        equationManager.ResetEquation();
    }
}
