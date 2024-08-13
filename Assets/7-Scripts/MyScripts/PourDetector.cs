using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 90; //threshold value for the angle of the bottle
    public Transform pourOrigin = null;
    public GameObject streamPrefab = null;

    public bool isPouring = false;
    private Stream currentStream = null;

    private void Update() 
    { 
        bool pourCheck = CalculatePourAngle() < pourThreshold;
        //checking what is the angle of the bottle, whether it is less than the threshold value or not

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck; //assigning the value of pourCheck to isPouring 

            if(isPouring) //if the bottle is pouring
            {
                StartPour();
            }
            else
            {
                EndPour();
            }   
        }
    }

    private void StartPour()
    {
        currentStream = CreateStream(); //creating a stream object
        currentStream.Begin(); //beginning the stream
    }

    private void EndPour()
    {
        currentStream.End(); //ending the stream
        currentStream = null; //setting the current stream to null to keep it clean.
    }

    private float CalculatePourAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg; //calculating the angle of the bottle at which it is tilting
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, pourOrigin.position, Quaternion.identity, transform);
        //creating a stream object at the position of pourOrigin, from where the pouring will start

        return streamObject.GetComponent<Stream>();
    }
}