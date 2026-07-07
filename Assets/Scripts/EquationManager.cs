using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EquationManager : MonoBehaviour
{

    public TMP_Text equationText;
    private string currentEquation = "";
    
    private List<int> numbers = new List<int>();
    private List<string> operators = new List<string>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddValue(string value)
    {
        if (value == "+" || value == "-")
        {
            if (operators.Count >= 1)
            {
                Debug.Log("Test");
                return false;
            }
            else
            {
            operators.Add(value);
            Debug.Log(string.Join(", ", operators));
            currentEquation += value + " ";
            equationText.text = currentEquation;
            return true;
            }
        }
        else if (int.TryParse(value, out int result))
        {
            if (numbers.Count >= 2)
            {
                Debug.Log("Test2");
                return false;
            }
            else
            {
            numbers.Add(result);
            Debug.Log(string.Join(", ", numbers));
            currentEquation += value + " ";
            equationText.text = currentEquation;
            return true;
            }
        }
        else
        {
            Debug.Log($"String passed is neither operator or number, {value}"); 
            return false;
        }
    }
}
