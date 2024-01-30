using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string sceneToLoad = "CinematicDialogues";
    public float delayBeforeSceneLoad = 2f;

    private bool hasEntered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {
            hasEntered = true;
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeSceneLoad);

        // Load the next scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
