using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWineGlass : MonoBehaviour
{
    public Transform liquidSurface;                                 // The surface of the liquid in the glass
    public float fillSpeed;                                         // The speed at which the liquid fills the glass
    public Transform topRimOfGlass;                                 // The transform representing the top rim of the glass
    public GameObject TaskEndUI;
    void Start()
    {
        liquidSurface = GetComponent<Transform>();                  //getting the transform component of the liquid surface where this script is attached
        
    }
    public void BeginFill()
    {
        Debug.Log("Begin Filling");
        StartCoroutine(FillGlass());
    }

    private IEnumerator FillGlass()
    {
        Debug.Log("Filling Glass");
        Vector3 startPosition = liquidSurface.localPosition;        // Get the current position of the liquid surface

        float limit = topRimOfGlass.localPosition.z;                // Set the limit to the Z position of the top rim of the glass (in this case we are using the z axis because of the orientation of the glass and the wine layer)
        
        while (liquidSurface.localPosition.z < limit)               // While the liquid surface is below the top rim
        {
            liquidSurface.localPosition += new Vector3(0, 0, fillSpeed * Time.deltaTime); // Move the liquid surface upward by a small amount (along z axis)

            if (liquidSurface.localPosition.z > limit)              // Ensure the liquid surface doesn't exceed the limit
            {
                liquidSurface.localPosition = new Vector3(startPosition.x, startPosition.y, limit);
                TaskEndUI.SetActive(true); //display the task end UI
                break;
            }

            yield return null;                                      // Wait until the next frame
        }
    }

}
