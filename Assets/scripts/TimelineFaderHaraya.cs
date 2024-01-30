using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimelineFaderHaraya : MonoBehaviour
{
    public float fadeDuration = 2f; // Adjust the duration of the fade
    private Graphic[] graphics;

    // Function to fade out the timeline and turn it off
    public void FadeOutAndLoadNextScene(string nextSceneName)
    {
        // Get all Graphic components attached to the GameObject
        graphics = GetComponentsInChildren<Graphic>();

        StartCoroutine(FadeOutAndLoadScene(nextSceneName));
    }

    // Coroutine for fading out the timeline and loading the next scene
    IEnumerator FadeOutAndLoadScene(string nextSceneName)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

            // Set the alpha for all Graphic components
            foreach (Graphic graphic in graphics)
            {
                Color currentColor = graphic.color;
                currentColor.a = alpha;
                graphic.color = currentColor;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure all Graphics have completely faded out
        foreach (Graphic graphic in graphics)
        {
            Color finalColor = graphic.color;
            finalColor.a = 0f;
            graphic.color = finalColor;
        }

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
