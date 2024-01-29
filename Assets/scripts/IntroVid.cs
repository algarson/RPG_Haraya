using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject mainMenu;
    public GameObject bg;


    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
       
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Video has ended, deactivate the video player and activate the main menu
        videoPlayer.gameObject.SetActive(false);
        mainMenu.SetActive(true);
        bg.SetActive(true);

}
}
