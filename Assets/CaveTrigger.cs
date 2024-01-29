using UnityEngine;

public class CaveTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    private bool playerInsideTrigger = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;  // Set the audio source to loop
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone");
            playerInsideTrigger = true;
            PlaySFX();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger zone");
            playerInsideTrigger = false;
            StopSFX();
        }
    }

    void PlaySFX()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void StopSFX()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
