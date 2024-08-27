using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Yarn.Unity;
using Yarn;

public class YarnUIHandler : MonoBehaviour
{
    //this script is attached to yarn conversation related UI, attached to the UI itself. 
    //if the respective variable from the yarn element is false, the UI will be enabled, if true, the UI will be disabled.

    
    public InMemoryVariableStorage yarnInMemoryStorage;
    public DialogueRunner dialogueRunner;
    private static bool isCommandRegistered = false; // Static flag to track registration

    public GameObject[] YarnUI_Elements; // Public reference to the UI element that should be enabled/disabled
    public GameObject[] MapPinThisTask; // Reference to the map pin object

    // Add a boolean flag to track whether the pin has been visited
    public bool[] hasBeenVisited;

    // Start is called before the first frame update
    void Awake()
    {
        if (dialogueRunner == null)
        {
            dialogueRunner = FindObjectOfType<DialogueRunner>();
        }

        if (yarnInMemoryStorage == null)
        {
            yarnInMemoryStorage = FindObjectOfType<InMemoryVariableStorage>();
        }

        // Register the command handler only if it's not already registered
        if (!isCommandRegistered)
        {
            dialogueRunner.AddCommandHandler<string>("update_ui", UpdateUIHandler);
            isCommandRegistered = true; // Set the flag to true after registration
        }
    }

    public void UpdateUIHandler(string variableName)
    {
        Debug.Log("Received command to update UI with value: " + variableName);

        switch (variableName)
        {
            case "$WilliamFinished": //welcome conversation
                UIHandler("$WilliamFinished", 0);
                break;

            case "$ThomasFinished": //sailing conversation
                UIHandler("$ThomasFinished", 1);
                break;

            case "$RobertFinished": //currency conversation
                UIHandler("$RobertFinished", 2);
                break;

            case "$SoldierFinished": //soldier,defence conversation
                UIHandler("$SoldierFinished", 3);
                break;
        }
        
    }

    public void UIHandler(string variableName, int i)
    {
        // Check if the variable exists in storage
        if (yarnInMemoryStorage.TryGetValue(variableName, out bool variableValue))
        {
            Debug.Log("Variable value from Yarn: " + variableValue + "for the variable name: " + variableName);
            
            YarnUI_Elements[i].SetActive(!variableValue); 
            // Enable UI when the variable is false, disable when true

            if (variableValue == true)
            {
                // Only change the pin color if it hasn't been visited yet
                if (hasBeenVisited[i] != true)
                {
                    // Change the color of the map pin
                    MapPinThisTask[i].GetComponent<Renderer>().material.color = Color.green;
                    Debug.Log("Map pin color changed to green.");

                    // Set the flag to indicate the pin has been visited
                    hasBeenVisited[i] = true;
                }
            }
            else
            {
                Debug.Log("UI element is enabled.");
            }
        }
        else
        {
            Debug.LogError($"Variable '{variableName}' not found in Yarn variable storage.");
        }
    }
}
