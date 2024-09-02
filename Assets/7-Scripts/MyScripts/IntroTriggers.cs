using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTriggers : MonoBehaviour
{
    //this script is attached to the trigger zone cube, and is used to play the audio when the player enters the trigger zone
    public AudioSource IntroAudioThisObj;
    private Collider cubeCollider; //will store the collider of the trigger zone cube

    public List<AudioSource> audioToKeepPlaying;  // List of audio sources that should keep playing even after others have stopped

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

        if (other.gameObject.CompareTag("Player"))  
        {
            Debug.Log($"Entered trigger zone: {gameObject.tag}");

            // Stop all other audio except the ones in ignoreAudioSources and this trigger's audio
            StopAllAudioExceptInList();

            // Play the audio for this trigger zone
            IntroAudioThisObj.Play();

            //only disable the collider if the scene is not the museum scene
            if(SceneManager.GetActiveScene().name != "5 Museum Scene")
            {
                // Disable the collider after the audio finishes playing
                StartCoroutine(DisableTriggerAfterAudio());
            }
            
        }
    }
    private IEnumerator DisableTriggerAfterAudio()
    {
        // Wait until the audio is finished playing
        yield return new WaitWhile(() => IntroAudioThisObj.isPlaying);

        // Remove the collider entirely
        Destroy(cubeCollider);
        Debug.Log("Collider has been removed after audio finished playing.");
    }

    //this function will make sure that only the audio of the trigger zone is playing and all other unnecessary audio is stopped
    private void StopAllAudioExceptInList()
    {
        // Find all audio sources in the scene
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        // Loop through all audio sources
        foreach (AudioSource audioSource in allAudioSources)
        {
            // Stop the audio source if it's not in the audioToKeepPlaying list and not the trigger's audio
            if (!audioToKeepPlaying.Contains(audioSource) && audioSource != IntroAudioThisObj)
            {
                audioSource.Stop();
            }
        }
    }

}
