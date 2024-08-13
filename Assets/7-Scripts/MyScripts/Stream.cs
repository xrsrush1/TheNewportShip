using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    public LineRenderer lineRenderer = null;

    public Vector3 targetPosition = Vector3.zero;

    public ParticleSystem splashParticles = null;

    private Coroutine pourRoutine = null;                               //will hold reference to the coroutine

    public FillWineGlass fillWineGlassScript = null;
    public AudioSource liquidPouringSound = null;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashParticles = GetComponentInChildren<ParticleSystem>();     //to get the splash particles which are the children of the stream

        fillWineGlassScript = FindObjectOfType<FillWineGlass>();        //finding the FillWineGlass script in the scene
        liquidPouringSound = GetComponent<AudioSource>();               //getting the audio source component of the liquid pouring sound

    }

    private void Start()
    {
        //to make sure the line renderer is in the correct position
        MoveToPosition(0, transform.position);                          //start position of the stream, target position is the position of the stream currently
        MoveToPosition(1, transform.position);                          //end position of the stream
    }

    public void Begin()
    {
        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPouring()); //assigning it to a variable so its easier to call 
    }

    private IEnumerator BeginPouring()
    {
        while(gameObject.activeSelf) //while the game object is active
        {
            targetPosition = FindEndPoint(); //find the end point of the stream
            MoveToPosition(0, transform.position); //keep the start of the stream same
            //MoveToPosition(1, targetPosition); //move the stream to the end point
            AnimatetoPosition(1, targetPosition); //animate the stream to the end point instead of just moving it
            yield return null;
        }
    }

    public void End()
    {

        StopCoroutine(pourRoutine); //stop the coroutine
        pourRoutine = StartCoroutine(EndPour()); //end the pour, assigning new coroutine to the variable, for consistency
    }

    private IEnumerator EndPour()
    {
        //once the pour is ended, the object will be destroyed
        while(!HasReachedPosition(0, targetPosition)) //while the stream has not reached the end point
        {
            AnimatetoPosition(0, targetPosition); //animate the start of the stream to the end point
            AnimatetoPosition(1, targetPosition); //animate the end of the stream to the end point
            liquidPouringSound.Stop(); //stop the liquid pouring sound
            yield return null;
        }
        
        //once the start of the stream have reached the end point, we will destroy the object
        Destroy(gameObject); //so the stream does not stay in the scene
    }
    private Vector3 FindEndPoint() //this function will find the end point of the stream
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down); //ray from the position of the stream to the down direction


        /* Physics.Raycast(ray, out hit, 2.0f); //raycast to find the hit point. 2.0f is the distance of the raycast

        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);
        //if the raycast hits something, the end point is the hit point, otherwise the end point is the position of the stream

        //wine glass fill logic starts here
        if (hit.collider.name == "WineLayer") //checking if the ray hit the wine layer
        {
            Debug.Log("Ray hit the wine layer!");
            liquidPouringSound.Play(); //play the liquid pouring sound
            fillWineGlassScript.BeginFill(); //begin filling the wine glass, in the fill wine script

        }
        return endPoint; //return the end point of the stream
        */


        // Perform the raycast
        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            // If the raycast hits something, get the point where the ray hit and store it in a var which is endPoint
            Vector3 endPoint = hit.point;

            // Check if the ray hit the "WineLayer"
            if (hit.collider != null && hit.collider.name == "WineLayer")
            {
                // Start playing the sound immediately when the first point hits the wine layer
                if (!liquidPouringSound.isPlaying) // Ensure the sound only starts once
                {
                    Debug.Log("Ray hit the wine layer!");
                    liquidPouringSound.Play(); // Play the liquid pouring sound
                    fillWineGlassScript.BeginFill(); // Begin filling the wine glass in the fill wine script
                }
            }

            return endPoint; // Return the point where the ray hit
        }
        else
        {
            // If the ray didn't hit anything, return the default end point
            return ray.GetPoint(2.0f); // Return the point 2 units down from the stream
        }
    }
    
    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition); //setting the position of the line renderer
    }

    private void AnimatetoPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index); //getting the current position of the index on the line renderer
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime *1.75f); //moving/animating the current position of the index on the line renderer towards the target position
        
        lineRenderer.SetPosition(index, newPosition); //setting the new position of the line renderer
    
    }

    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index); //getting the current position of the index on the line renderer
        return currentPosition == targetPosition; //returning true if the current position is equal to the target position
        
    }

    private IEnumerator UpdateParticle()
    {
        //to manage the position of splash particles

        while(gameObject.activeSelf) //while the game object is active
        {
            splashParticles.gameObject.transform.position = targetPosition; //setting the position of the splash particles to the target position
            
            //we want to show the splash particles only when the start point of stream first reaches the end point
            bool isHitting = HasReachedPosition(1, targetPosition); //checking if the start point of the stream has reached the end point

            splashParticles.gameObject.SetActive(isHitting); 
            //activating the splash particles only when the start point of the stream has reached the end point
            //basically when isHitting is true
            yield return null;
        }
    }
}
