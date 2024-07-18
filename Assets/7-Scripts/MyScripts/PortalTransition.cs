using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTransition : MonoBehaviour
{
    public string sceneToLoad; // The name of the scene to load

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

        // Wait for the fade-in to complete
        yield return new WaitForSeconds(1f);

        // Load the new scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
