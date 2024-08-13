using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxRotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Speed of the skybox rotation

    void Update()
    {
        // Rotate the skybox over time
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
