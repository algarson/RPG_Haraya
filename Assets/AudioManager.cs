using UnityEngine;
using UnityEngine.Video;
using System.Collections; // Make sure to include this

public class AudioManager : MonoBehaviour
{
    public VideoPlayer introVideoPlayer; // VideoPlayer component for intro video
    public AudioSource bgMusic;          // AudioSource component for background music

    public float delayBeforeBgMusic = 1.0f; // Delay in seconds before starting background music
    public float fadeDuration = 1.0f;       // Duration of the fade-in effect

    void Start()
    {
        // Set up a callback to start background music when the intro video is finished
        introVideoPlayer.loopPointReached += EndIntroVideo;

        // Play intro video
        introVideoPlayer.Play();
    }

    // Callback function when the loop point of the intro video is reached
    void EndIntroVideo(VideoPlayer vp)
    {
        // Stop the intro video playback (optional)
        vp.Stop();

        // Start the delayed fade-in of the background music
        StartCoroutine(StartBgMusicWithFade());
    }

    // Coroutine to handle delayed fade-in of the background music
    IEnumerator StartBgMusicWithFade()
    {
        yield return new WaitForSeconds(delayBeforeBgMusic);

        float elapsedTime = 0f;
        float startVolume = 0f;

        // Fade in the background music
        while (elapsedTime < fadeDuration)
        {
            bgMusic.volume = Mathf.Lerp(startVolume, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the volume is set to 1 after the fade-in
        bgMusic.volume = 1f;

        // Start playing the background music
        bgMusic.Play();
    }
}
