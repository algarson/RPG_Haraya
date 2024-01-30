using UnityEngine;
using UnityEngine.Playables;

public class DisableTimeline : MonoBehaviour
{
    public PlayableDirector playableDirector;

    void Start()
    {
        if (playableDirector != null)
        {
            // Subscribe to the stopped event
            playableDirector.stopped += OnTimelineStopped;
        }
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        // This method will be called when the timeline stops playing
        Debug.Log("Timeline stopped playing");

        // Disable the PlayableDirector component or the GameObject containing it
        if (playableDirector != null)
        {
            playableDirector.enabled = false; // Disable the PlayableDirector component
            // Alternatively, you can disable the entire GameObject:
            // gameObject.SetActive(false);
        }
    }
}