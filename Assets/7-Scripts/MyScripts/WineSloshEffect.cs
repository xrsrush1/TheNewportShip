using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WineSloshEffect : MonoBehaviour
{
    public GameObject LiquidLayerParent; //the parent object of the liquid layers
    public GameObject LiquidLayerMainMesh; //the main mesh of the liquid layer
    
    public int sloshSpeed = 60; //the speed at which the liquid will slosh
    public int rotateSpeed = 15; //the speed at which the liquid will rotate

    public float difference = 0.01f; //will be used to clamp the rotation of the liquid layer

    private Quaternion initialRotation; // Store the initial rotation of the parent

    void Start()
    {
        // Ensure the initial rotation is set to identity
        initialRotation = Quaternion.identity;
        LiquidLayerParent.transform.localRotation = initialRotation;
    }
    void Update()
    {
        // Slosh the liquid based on mug movement
        Slosh();

        // Rotate the liquid layer mesh slowly for visual effect
        LiquidLayerMainMesh.transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed, Space.Self);

    }

    private void Slosh()
    {
        //inverse local rotation of the wine glass
        Quaternion inverseRotation = Quaternion.Inverse(transform.localRotation);

        // Rotate towards inverse of the wine mug's rotation
        Quaternion targetRotation = Quaternion.RotateTowards(LiquidLayerParent.transform.localRotation, inverseRotation, sloshSpeed * Time.deltaTime);
        
        // Convert target rotation to Euler angles
        Vector3 finalRotation = targetRotation.eulerAngles;

        // Check if the mug is stationary (within a small threshold)
        if (transform.localEulerAngles != Vector3.zero)
        {
            // Clamp the rotation of the liquid layer so the internal mesh isn't flying around
            finalRotation.x = ClampRotationValue(finalRotation.x, difference);
            finalRotation.y = ClampRotationValue(finalRotation.y, difference);
            finalRotation.z = ClampRotationValue(finalRotation.z, difference);
        }
        else
        {
            // Smoothly reset rotation to zero when stationary
            finalRotation = Vector3.Lerp(LiquidLayerParent.transform.localEulerAngles, Vector3.zero, Time.deltaTime * sloshSpeed);
        }
        
        // Apply the clamped rotation to the liquid layer parent
        LiquidLayerParent.transform.localEulerAngles = finalRotation;

    }

    private float ClampRotationValue(float value, float difference)
    {
        //here we will clamp the local rotation of the liquid layer
        //we are working with local rotation, so the values will be between 0 and 360

        float returnValue = 0.0f;

        if (value > 180)
        {
            returnValue = Mathf.Clamp(value, 360 - difference, 360);
        }
        else
        {
            returnValue = Mathf.Clamp(value, 0, difference);
        }

        return returnValue;
    }
}
