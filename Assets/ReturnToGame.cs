using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToGame : MonoBehaviour
{
    public string mainGameSceneName = "haraya"; // Specify the name of the main game scene

    void Start()
    {
        // Load the main game scene additively
        SceneManager.LoadScene(mainGameSceneName, LoadSceneMode.Additive);
    }

    void Update()
    {
        // Check for a key press or any other condition to trigger the return to the main game scene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainGameScene();
        }
    }

    void ReturnToMainGameScene()
    {
        // Unload the current scene
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
