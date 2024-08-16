using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxRotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Speed of the skybox rotation
    public float rotationValue = 0.0f; // This will store the desired rotation angle for the skybox

    private void Start()
    {
        RenderSettings.skybox.SetFloat("_Rotation", rotationValue);
    }
    void Update()
    {
        // Rotate the skybox over time
        //RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
        
    }
}
