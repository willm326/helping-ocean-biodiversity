using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    private AudioSource typingSound;
    [SerializeField]
    private Image environment;
    [SerializeField]
    private Image character;
    [SerializeField]
    private Image cinematic;
    [SerializeField]
    private Text speakerName;
    [SerializeField]
    private Text dialogueText;

    private Queue<string> sentences = new Queue<string>();

    public EventHandler QueueIsEmpty; //This action is emitted when there are no sentences left to read in the queue

    public void DisplayNextSentence()
    {
        //TODO: immediately show the full line instead of proceeding to the next one if the coroutine is running

        if (sentences.Count != 0)
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        else
        {
            QueueIsEmpty?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ReadScenario(Scenario scenario)
    {
        if (scenario == null)
        {
            Debug.LogError("Provided scenario does not contain a reference, failed to read");
            return;
        }

        if (sentences.Count > 0)
        {
            Debug.LogWarning("Last scenario never finished reading, removing remaining sentences from the queue");
            sentences.Clear();
        }

        environment.sprite = scenario.Environment;
        character.sprite = scenario.Character;
        cinematic.sprite = scenario.Overlay;

        speakerName.text = scenario.Speaker;

        sentences.Clear();

        foreach (string sentence in scenario.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void EnqueueSentence(string sentence)
    {
        sentences.Enqueue(sentence);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if (typingSound.isPlaying == false && letter.ToString() != " ")
            {
                typingSound.UnPause();
            }
            if (letter.ToString() == "." || letter.ToString() == "?" || letter.ToString() == "M" || letter.ToString() == "\"")
            {
                typingSound.Pause();
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
