using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimelineFader : MonoBehaviour
{
    public float fadeDuration = 2f; // Adjust the duration of the fade
    private Graphic[] graphics;

    // Function to fade out the timeline and turn it off
    public void FadeOutAndTurnOffTimeline()
    {
        // Get all Graphic components attached to the GameObject
        graphics = GetComponentsInChildren<Graphic>();

        StartCoroutine(FadeOutAndTurnOff());
    }

    // Coroutine for fading out the timeline and turning it off
    IEnumerator FadeOutAndTurnOff()
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

        // Turn off the timeline by deactivating the GameObject
        gameObject.SetActive(false);
    }
}
