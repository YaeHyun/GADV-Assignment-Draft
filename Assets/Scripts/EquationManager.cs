using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EquationManager : MonoBehaviour
{

    public TMP_Text equationText;
    
    private List<int> numbers = new List<int>();
    private List<string> operators = new List<string>();

    private string leftNumber;
    private string op;
    private string rightNumber;
    private int result;
    public int finalNumber = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIEquationUpdate();
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
            UIEquationUpdate();
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
            UIEquationUpdate();
            return true;
            }
        }
        else
        {
            Debug.Log($"String passed is neither operator or number, {value}"); 
            return false;
        }
    }

    private void UIEquationUpdate()
    {
        if (numbers.Count > 0)
        {
            leftNumber = numbers[0].ToString();
        }
        else
        {
            leftNumber = "_";
        }

        if (operators.Count > 0)
        {
            op = operators[0];
        }
        else
        {
            op = "_";
        }

        if (numbers.Count > 1)
        {
            rightNumber = numbers[1].ToString();
        }
        else
        {
            rightNumber = "_";
        }

        equationText.text = $"{leftNumber} {op} {rightNumber} = {finalNumber.ToString()}";
    }

    public bool EquationCheck()
    {
        if (numbers.Count != 2)
        {
            Debug.Log("Need 2 numbers");
            return false;
        }

        if (operators.Count != 1)
        {
            Debug.Log("Needs operator");
            return false;
        }

        if (op == "+")
        {
            result = numbers[0] + numbers[1];
        }
        else if (op == "-")
        {
            result = numbers[0] - numbers[1];
        }

        if (result == finalNumber)
        {
            Debug.Log("Correct");
            return true;
        }
        else
        {
            Debug.Log("Wrong");
            return false;
        }
    }

    public void ResetEquation()
    {
        numbers.Clear();
        operators.Clear();
        UIEquationUpdate();
    }
}

