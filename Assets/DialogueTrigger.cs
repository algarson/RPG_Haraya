using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool dialogueTriggered = false;

    // Assuming you have a reference to the PlayableDirector
    public PlayableDirector playableDirector;


    private void Start()
    {
       
    }

    private void OnActivationFinished()
    {
        if (!dialogueTriggered)
        {
            TriggerDialogue();
            dialogueTriggered = true;
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
