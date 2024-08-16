using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinChangeAtTaskEnd : MonoBehaviour
{
    // This script will be used to change the color of the map pins at the end of the task
    public GameObject MapPinThisTask; // Reference to the map pin object
    private void Awake()
    {
        // Change the color of the map pins
        ChangePinColor();
    }
    
    private void ChangePinColor()
    {
        // Change the color of the map pin
        MapPinThisTask.GetComponent<Renderer>().material.color = Color.green;
        
    }
}
