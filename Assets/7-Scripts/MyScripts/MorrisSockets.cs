using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MorrisSockets : MonoBehaviour
{
    //this script is attached to the socket object, and is used to check if the correct piece is placed in the socket

    public string correctPieceTag;                         // Tag of the correct piece that should be placed in the socket
    public XRSocketInteractor socketInteractor;     // Reference to the socket interactor

    public AudioSource correctSound;                // Sound to play if the correct object is placed
    public AudioSource wrongSound;                  // Sound to play if the wrong object is placed

    public GameObject CorrectUI;                    // Reference to the correct UI object
    public GameObject WrongUI;                      // Reference to the wrong UI object

    public MorrisEndGameChecker endGameChecker;     // Reference to the end game checker script
    public void Start()
    {
        // Ensure that the socket interactor is assigned
        if (socketInteractor == null)
        {
            socketInteractor = GetComponent<XRSocketInteractor>();
        }
        if(endGameChecker == null)
        {
            endGameChecker = FindObjectOfType<MorrisEndGameChecker>();
        }
    }
    public void CheckPiecePlaced()
    {
        // Get the object currently in the socket
        IXRSelectInteractable placedInteractable = socketInteractor.GetOldestInteractableSelected();

        GameObject placedObject = placedInteractable.transform.gameObject;

        // Check if the object in the socket has the correct tag
        if (placedObject.CompareTag(correctPieceTag))
        {
            Debug.Log("Correct piece placed!");
            correctSound.Play();
            CorrectUI.SetActive(true);  // Show the correct UI object
            endGameChecker.correctPiecesPlaced++;  // Increment the number of correct pieces placed
        }
        else
        {
            Debug.Log("Incorrect piece placed!");
            wrongSound.Play();  // Play the wrong sound continuously
            WrongUI.SetActive(true);  // Show the wrong UI object
        }
    }

    // This method will be called when the object is removed from the socket
    public void OnPieceRemoved()
    {
        if (WrongUI != null && WrongUI.activeSelf) //if the wrong UI object is active{
        {
            Debug.Log("Wrong object removed, sound stopped.");
            WrongUI.SetActive(false);  // Hide the wrong UI object

        }
    }
}
