using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public VideoPlayer introVideoPlayer; // VideoPlayer component for intro video

    public float fadeDuration = 0f;       // Duration of the fade-in effect
    public float delayBeforeIntroVideoPlayer = 0f; // Delay in seconds before starting intro video

    void Start()
    {
        // Set up a callback to start background music when the intro video is finished
        introVideoPlayer.loopPointReached += EndIntroVideo;

        // Delay before starting intro video
        StartCoroutine(DelayedStartIntroVideo());
    }

    // Coroutine to handle delayed start of the intro video
    IEnumerator DelayedStartIntroVideo()
    {
        Debug.Log("Delaying intro video by " + delayBeforeIntroVideoPlayer + " seconds.");
        yield return new WaitForSeconds(delayBeforeIntroVideoPlayer);

        // Play intro video
        introVideoPlayer.Play();
    }

    // Callback function when the loop point of the intro video is reached
    void EndIntroVideo(VideoPlayer vp)
    {
        // Stop the intro video playback (optional)
        vp.Stop();
  
    }

}
