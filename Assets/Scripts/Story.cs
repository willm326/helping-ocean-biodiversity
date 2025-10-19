using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    public Dialogue dialogue;
    public int morals;
    private bool dialogueHasBeenTriggered = false;

    private void Update()
    {
        if (WorldVariables.currentScenario == dialogue.Scenario && !dialogueHasBeenTriggered)
        {
            TriggerDialogue();
            dialogueHasBeenTriggered = true;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    
}
