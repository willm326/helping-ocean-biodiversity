using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueState
{
    ReadingScenario, //The Scenario's sentences are being read
    MakingChoice, //The player has buttons to make a decision, reading should stop
    ReadingResponse //The response to the player's choice is being read
}

public class NewDialogueManager : MonoBehaviour
{
    public AudioSource Type;
    private Queue<string> sentences;
    public Image Environment;
    public Image Character;
    public Image Cinematic;
    public Text Name;
    public Text dialogueText;
    public Scenario NextScenario;
    public Button ChoiceButton;
    public GameObject ButtonLayer;

    DialogueState currentState = DialogueState.ReadingScenario;
    Scenario currentScenario;

    void Start()
    {
        sentences = new Queue<string>();
        readScenario(NextScenario);
    }

    public void DisplayNextSentence()
    {
        if (currentState != DialogueState.MakingChoice)
        {
            if (sentences.Count != 0)
            {
                string sentence = sentences.Dequeue();
                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence));
            }
            else
            {
                endDialogue();
            }
        }
    }

    void endDialogue()
    {
        if (currentState == DialogueState.ReadingScenario)
        {
            createButtons();
        }
        else //state should be ReadingResponse
        {
            readScenario(NextScenario);
        }
    }

    void readScenario(Scenario scenario)
    {
        currentState = DialogueState.ReadingScenario;

        if (scenario != null)
        {
            currentScenario = scenario;
        }
        else
        {
            currentScenario = currentScenario.DefaultScenario;
        }

        Environment.sprite = currentScenario.Environment;
        Character.sprite = currentScenario.Character;
        Cinematic.sprite = currentScenario.Overlay;

        Name.text = currentScenario.Speaker;

        sentences.Clear();

        foreach (string sentence in currentScenario.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    void readChoice(Choice choice)
    {
        currentState = DialogueState.ReadingResponse;

        WorldVariables.morals += choice.MoralValue;

        NextScenario = choice.NextScenario;

        sentences.Clear();

        foreach (string sentence in choice.ResponseSentences)
        {
            sentences.Enqueue(sentence);
        }

        //choice.Selected = true;

        DisplayNextSentence();
    }

    void createButtons()
    {
        currentState = DialogueState.MakingChoice;

        if (currentScenario.Choices.Count == 0)
        {
            readScenario(currentScenario.DefaultScenario);
        }
        else
        {
            for (int i = 0; i < currentScenario.Choices.Count; i++)
            {
                if (!currentScenario.Choices[i].Selected)
                {
                    Button newButton = Instantiate(ChoiceButton, ButtonLayer.transform);
                    newButton.GetComponentInChildren<Text>().text = currentScenario.Choices[i].ButtonText;

                    int index = i; //Used to specify index of Choices array. If i is used all of the functions will use the final value of i
                    newButton.onClick.AddListener(() =>
                    {
                        readChoice(currentScenario.Choices[index]);
                    });
                }
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if (Type.isPlaying == false && letter.ToString() != " ")
            {
                Type.UnPause();
            }
            if (letter.ToString() == "." || letter.ToString() == "?" || letter.ToString() == "M" || letter.ToString() == "\"")
            {
                Type.Pause();
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
