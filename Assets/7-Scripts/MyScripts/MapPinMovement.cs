using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPinMovement : MonoBehaviour
{
    //this script will make the map pin move up and down

    public float speed = 1.8f;              // Speed of the movement
    public float maxHeight = 0.3f;          // Maximum height to oscillate above the starting position
    private Vector3 startPosition;          // Starting position of the object

    void Start()
    {
        startPosition = transform.position; // Store the initial position of the object
    }

    void Update()
    {
        // Calculate the new Y position based on a sine wave function
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * (maxHeight / 2) + (maxHeight / 2);

        // Update the position of the object
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
