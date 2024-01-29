using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image portraitImage;

    [SerializeField] private string[] speaker;
    [SerializeField] [TextArea] private string[] dialogueWords;
    [SerializeField] private Sprite[] portrait;

    private bool dialogueActivated;

    void Update()
    {
        if (Input.GetButtonDown("Interact") && dialogueActivated)
        {
            if (dialogueCanvas != null)
            {
                dialogueCanvas.SetActive(true);
                speakerText.text = speaker[0];
                dialogueText.text = dialogueWords[0];
                portraitImage.sprite = portrait[0];
                Debug.Log("Dialogue Canvas activated");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogueActivated = true;
            Debug.Log("Player entered the trigger zone");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogueActivated = false;
            if (dialogueCanvas != null)
            {
                dialogueCanvas.SetActive(false);
                Debug.Log("Player exited the trigger zone and Dialogue Canvas deactivated");
            }
        }
    }
}
