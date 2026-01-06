using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>DialogueBox</c> reads <c>Scenario</c> data, and displays the text to the player on a screen along with the corresponding graphics.
/// Uses a Next <c>Button</c> that the player uses to move to the next sentence or <c>Scenario</c>.
/// </summary>


public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    private Image environment;
    [SerializeField]
    private Image character;
    [SerializeField]
    private Image overlay; //Image that appears on top of both the enviornment and character, used for special full screen images or transparent overlays
    [SerializeField]
    private Text speakerName;
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private AudioSource typingSound;

    private Queue<string> sentences = new Queue<string>();
    private bool coroutineIsRunning = false;
    private string currentSentence;
    private bool hasChoices;

    public EventHandler QueueIsEmpty; //This action is emitted when there are no sentences left to read in the queue

    public void DisplayNextSentence()
    {
        if (coroutineIsRunning)
        {
            showFullSentence();
        }
        else if (sentences.Count != 0)
        {
            string sentence = sentences.Dequeue();
            StartCoroutine(typeSentence(sentence));
        }
        else if (!hasChoices)
        {
            nextButton.interactable = false;
            QueueIsEmpty?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.LogWarning("No sentence to display");
        }
    }

    /// <summary>
    /// Adds all of the sentences of the provided scenario to the queue, and immediately displays the graphics 
    /// associated with the provided scenario. Will remove any leftover sentences from the queue if there are any remaining.
    /// </summary>
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
        overlay.sprite = scenario.Overlay;

        speakerName.text = scenario.Speaker;

        hasChoices = scenario.Choices.Count != 0;

        //If the current scenario has no choices, but the next scenario has no text, its choices should be shown while the current scenario's text is on screen
        if (!hasChoices && scenario.DefaultScenario?.Sentences.Length == 0)
        {
            hasChoices = true;
        }

        foreach (string sentence in scenario.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        if (scenario.Sentences.Length != 0)
        {
            DisplayNextSentence();
            nextButton.interactable = true;
        }
        else
        {
            nextButton.interactable = false;
            QueueIsEmpty?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Used to add sentences to the queue without changing scenario graphics or removing any sentences from the queue.
    /// </summary>
    public void EnqueueSentence(string sentence)
    {
        nextButton.interactable = true;
        sentences.Enqueue(sentence);
        hasChoices = false;
    }

    private IEnumerator typeSentence(string sentence)
    {
        currentSentence = sentence;
        coroutineIsRunning = true;

        Color dialogueColor = dialogueText.color;
        dialogueText.color = dialogueColor;
        dialogueText.text = "<color=#00000000>" + sentence + "</color>";

        for (int i = 0; i < sentence.Length; i++)
        {
            dialogueText.text = sentence.Substring(0, i) + "<color=#00000000>" + sentence.Substring(i) + "</color>";

            if (typingSound.isPlaying == false && sentence[i].ToString() != " ")
            {
                typingSound.UnPause();
            }
            if (sentence[i].ToString() == "." || sentence[i].ToString() == "?" || sentence[i].ToString() == "\"")
            {
                typingSound.Pause();
            }
            yield return new WaitForSeconds(0.01f);
        }

        dialogueText.text = sentence;
        coroutineIsRunning = false;

        if (hasChoices && sentences.Count == 0)
        {
            nextButton.interactable = false;
            QueueIsEmpty?.Invoke(this, EventArgs.Empty);
        }
    }

    private void showFullSentence()
    {
        StopAllCoroutines();
        coroutineIsRunning = false;
        dialogueText.text = currentSentence;
        typingSound.Pause();

        if (hasChoices && sentences.Count == 0)
        {
            nextButton.interactable = false;
            QueueIsEmpty?.Invoke(this, EventArgs.Empty);
        }
    }
}
