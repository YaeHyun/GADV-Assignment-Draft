using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EquationManager : MonoBehaviour
{

    public TMP_Text equationText; // Gets the equation UI 
    public GameObject collectibleContainer; // Gets the container of all the collectibles

    
    private List<int> numbers = new List<int>(); // Creates list to store all numbers collected
    private List<string> operators = new List<string>(); // Creates list to store operator collected

    private string leftNumber; // Stores each of the number/operator currently collected to display in the UI
    private string op;
    private string rightNumber;
    private int result; // The final result of the equation through numbers collected
    public int finalNumber = 20; // The final number displayed the player is supposed to get 

    void Start()
    {
        UIEquationUpdate(); // Updates the UI to show the target number
    }

    public bool AddValue(string value) // Used to add a value to the equation
    {
        if (value == "+" || value == "-" || value == "x" || value == "/") // Checks if the value is an operator
        {
            if (operators.Count >= 1) // Checks if the operator list is full
            {
                Debug.Log("Test");
                return false;
            }
            else
            {
            operators.Add(value); // Adds the value if it is not full
            Debug.Log(string.Join(", ", operators));
            UIEquationUpdate(); // Updates the UI to reflect changes
            return true;
            }
        }
        else if (int.TryParse(value, out int result)) // Checks if the value is an integer
        {
            if (numbers.Count >= 2) // Checks if the number list has more than 2 values
            {
                Debug.Log("Test2");
                return false;
            }
            else
            {
            numbers.Add(result); // Adds the value to the list
            Debug.Log(string.Join(", ", numbers));
            UIEquationUpdate(); // Updates the UI to reflect changes
            return true;
            }
        }
        else
        {
            Debug.Log($"String passed is neither operator or number, {value}"); 
            return false;
        }
    }

    private void UIEquationUpdate() // Updates the equation UI
    {
        if (numbers.Count > 0) // Checks if number list is empty
        {
            leftNumber = numbers[0].ToString(); // Assigns the value to the left number variable and converts to string
        }
        else
        {
            leftNumber = "_"; // Sets the UI to empty
        }

        if (operators.Count > 0) // Checks if operator list is empty
        {
            op = operators[0];  // Assigns the value to the operator
        }
        else
        {
            op = "_"; // Sets the UI to empty
        }

        if (numbers.Count > 1) // Checks if list only has 1 value
        {
            rightNumber = numbers[1].ToString(); // Assigns value to the right number variable and converts to string
        }
        else
        {
            rightNumber = "_"; // Sets the UI to empty
        }

        equationText.text = $"{leftNumber} {op} {rightNumber} = {finalNumber.ToString()}"; // Updates the UI text
    }

    public bool EquationCheck() // Checks the equation if it is valid and if it matches the target number
    {
        if (numbers.Count != 2) // Does not have 2 numbers 
        {
            Debug.Log("Need 2 numbers");
            return false;
        }

        if (operators.Count != 1) // Does not have a operator
        {
            Debug.Log("Needs operator");
            return false;
        }

        if (op == "+") // Finds the correct operator and does the proper operation on equation
        {
            result = numbers[0] + numbers[1];
        }
        else if (op == "-")
        {
            result = numbers[0] - numbers[1];
        }
        else if (op == "x")
        {
            result = numbers[0] * numbers[1];
        }
        else if (op == "/")
        {
            result = numbers[0] / numbers[1];
        }

        if (result == finalNumber) // Checks the result to the target number and returns a boolean value
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

    public void ResetEquation() // Resets the equation when player submits a wrong equation or manually resets the equation
    {
        numbers.Clear(); // Clears the number and operator lists
        operators.Clear();
        UIEquationUpdate();
        ResetCollectibles();
    }

    void ResetCollectibles() // Resets the collectibles on the map such that they can be collected again
    {
        foreach(Transform child in collectibleContainer.transform) // Iterates through every collectible in the collectible container
        {
            CollectableScript collectible = child.GetComponent<CollectableScript>(); // References the collectable script 
            collectible.ResetCollectible(); // Uses the reset function in the script
        }
    }
}

