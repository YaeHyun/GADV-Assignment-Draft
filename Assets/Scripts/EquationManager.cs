using UnityEngine;
using TMPro;

public class EquationManager : MonoBehaviour
{

    public TMP_Text equationText;
    private string currentEquation = "";
    
    private string TargetNumber;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddValue(string value)
    {
        currentEquation += value + " ";
        equationText.text = currentEquation;
    }
}
