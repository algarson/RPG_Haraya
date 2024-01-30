using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    public string nextSceneName = "haraya"; // Specify the name of the scene to load
    public float delayBeforeLoading = 3f; // Set the delay before transitioning to the next scene

    void Start()
    {
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoading);

        SceneManager.LoadScene(nextSceneName);
    }
}
