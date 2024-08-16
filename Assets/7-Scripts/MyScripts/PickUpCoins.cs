using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PickUpCoins : MonoBehaviour
{
    public InputActionReference leftTriggerAction; //ref to the left trigger input action, here: XRI LeftHand Interaction/Activate
    public InputActionReference rightTriggerAction; //ref to the right trigger input action, here: XRI RightHand Interaction/Activate

    public bool inCoinPickupZone = false; //flag to check if the player is in the coin pickup zone


    // Start is called before the first frame update
    void Start()
    {
        //subscribe to the trigger press event
        leftTriggerAction.action.performed += TriggerPressed;
        rightTriggerAction.action.performed += TriggerPressed;

        //subscribe to the trigger release event
        leftTriggerAction.action.canceled += TriggerReleased;
        rightTriggerAction.action.canceled += TriggerReleased;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with " + other.gameObject.name);    

        if (other.gameObject.name == "RightHand-Collider" || other.gameObject.name == "LeftHand-Collider")
        {
            inCoinPickupZone = true;
            Debug.Log("Player is in the coin pickup zone? : " + inCoinPickupZone);
        }
    }
    private void OnDestroy()
    {
        //unsubscribe from the trigger press event
        leftTriggerAction.action.performed -= TriggerPressed;
        rightTriggerAction.action.performed -= TriggerPressed;

        //unsubscribe from the trigger release event
        leftTriggerAction.action.canceled -= TriggerReleased;
        rightTriggerAction.action.canceled -= TriggerReleased;
    }

    //method to handle the trigger press event
    private void TriggerPressed(InputAction.CallbackContext obj)
    {
        if(inCoinPickupZone)
        {
            Debug.Log("Player picked up the coin");
            //here, pick the coin by matching the coin's position and rotation to the hand's tip's position and rotation

        }
    }

    //method to handle the trigger release event
    private void TriggerReleased(InputAction.CallbackContext obj)
    {
        //here just drop the coin, by making the coin use gravity and disabling kinematic
        Debug.Log("Trigger Released");
    }
}
