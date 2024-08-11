using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTriggers : MonoBehaviour
{
    public AudioSource IntroAudioThisObj;
    private Collider cubeCollider; //will store the collider of the trigger zone cube

    // Start is called before the first frame update
    void Start()
    {
        IntroAudioThisObj = GetComponent<AudioSource>();
        cubeCollider = GetComponent<Collider>();  // Get the collider of the cube
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        //this will play the audio when the player enters the trigger zone
        //as audio is unique to each trigger zone, we dont have to use the switch case to check tags to play the audio

        Debug.Log("Entered OnTriggerEnter");

        if (other.gameObject.CompareTag("Player"))  
        {
            Debug.Log($"Entered trigger zone: {gameObject.tag}");
            IntroAudioThisObj.Play();

            StartCoroutine(DisableTriggerAfterAudio());  // Start coroutine to disable IsTrigger after audio finishes
        }
    }
    private IEnumerator DisableTriggerAfterAudio()
    {
        // Wait until the audio is finished playing
        yield return new WaitWhile(() => IntroAudioThisObj.isPlaying);

        // Disable the IsTrigger property of the collider
        cubeCollider.isTrigger = false;
        Debug.Log("IsTrigger has been disabled after audio finished playing.");
    }
}
