using UnityEngine;
using UnityEngine.Playables;

public class ActivationTrackListener : MonoBehaviour
{
    public delegate void ActivationFinishedDelegate();
    public event ActivationFinishedDelegate OnActivationFinished;

    private PlayableDirector playableDirector;
    private bool activationTrackFinished = false;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineStopped;
        }
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if (director.playableAsset.name == "Dialogue") 
        {
            activationTrackFinished = true;
            if (OnActivationFinished != null)
            {
                OnActivationFinished();
            }
        }
    }
}
