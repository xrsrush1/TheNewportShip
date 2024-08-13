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
    
    private int difference = 25; //will be used to clamp the rotation of the liquid layer

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //motion of the liquid layer
        Slosh();

        //rotation of the liquid layer, will slowly rotate the liquid layer
        LiquidLayerMainMesh.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.Self);

    }

    private void Slosh()
    {
    
        //inverse local rotation of the wine glass
        Quaternion inverseRotation = Quaternion.Inverse(transform.localRotation);

        //rotate to:
        Vector3 finalRotation = Quaternion.RotateTowards(LiquidLayerParent.transform.localRotation, inverseRotation, sloshSpeed * Time.deltaTime).eulerAngles;
        //geting the euler angles of the final rotation

        //clamp the rotation of the liquid layer, so the internal mesh isnt flying around
        finalRotation.x = ClampRotationValue(finalRotation.x, difference);
        finalRotation.y = ClampRotationValue(finalRotation.y, difference);

        //set the rotation of the liquid layer to the final rotation
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
            //if the value is greater than 180, we will clamp it between 360-difference (360-25 = 335) and 360
        }
        else
        {
            returnValue = Mathf.Clamp(value, 0, difference);
            //if the value is less than 180, we will clamp it between 0 and difference (25)
        }

        return returnValue;
    }
}
