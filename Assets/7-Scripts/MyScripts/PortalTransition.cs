using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTransition : MonoBehaviour
{
    public string sceneToLoad;                          // The name of the scene to load
    public AudioSource TransitionAudioSource;           // The audio source to play the transition sound

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        // Display black screen effect
        //BlackScreenController.instance.FadeIn();

        // Play the transition sound
        TransitionAudioSource.Play();

        // Wait for the fade-in to complete
        yield return new WaitForSeconds(1f);

        // Load the new scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
