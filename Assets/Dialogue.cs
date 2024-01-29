using System.Collections;
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
    [SerializeField] private int step;

    private bool dialogueActivated;
    private int currentLine = 0;

    void Update()
    {
        if (dialogueActivated)
        {
            if (dialogueCanvas != null)
            {
                dialogueCanvas.SetActive(true);

                if (speakerText != null && speaker.Length > 0 && currentLine < speaker.Length)
                    speakerText.text = speaker[currentLine];
                else
                    Debug.LogWarning("speakerText is null or speaker array is empty");

                if (dialogueText != null && dialogueWords.Length > 0 && currentLine < dialogueWords.Length)
                    dialogueText.text = dialogueWords[currentLine];
                else
                    Debug.LogWarning("dialogueText is null or dialogueWords array is empty");

                if (portraitImage != null && portrait.Length > 0 && currentLine < portrait.Length)
                    portraitImage.sprite = portrait[currentLine];
                else
                    Debug.LogWarning("portraitImage is null or portrait array is empty");
            }
            else
            {
                Debug.LogWarning("dialogueCanvas is null");
            }
        }
        else
        {
            Debug.Log("Interact not pressed or dialogue not activated");
        }
    }
    public void NextLine()
    {
        Debug.Log("NextLine method called.");

        if (currentLine < dialogueWords.Length - 1)
        {
            currentLine++;
            dialogueText.text = dialogueWords[currentLine];
            Debug.Log("Next Line: " + dialogueText.text);
        }
        else
        {
            // You've reached the end of the dialogue, you can handle it accordingly
            EndDialogue();
        }
    }


    private void EndDialogue()
    {
        // Implement any logic you want when the dialogue ends
        Debug.Log("End of dialogue");
        dialogueCanvas.SetActive(false);
        dialogueActivated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogueActivated = true;
            currentLine = 0;
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
