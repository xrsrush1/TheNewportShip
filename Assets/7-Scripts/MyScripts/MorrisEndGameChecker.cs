using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorrisEndGameChecker : MonoBehaviour
{
    public int correctPiecesPlaced = 0; // Number of correct pieces placed
    
    public GameObject EndGameUI; // Reference to the end game UI object

    private void Update()
    {
        // Check if all the correct pieces have been placed
        if (correctPiecesPlaced == 4)
        {
            Debug.Log("All correct pieces placed, end game!");
            EndGameUI.SetActive(true);  // Show the end game UI object
            // End the game
        }
    }
}
