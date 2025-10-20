using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewDialogueManager : MonoBehaviour
{
    public AudioSource Music;
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
    public Image Fish;
    public Image Shark;
    public Image MonkSeal;
    public Image Turtle;
    public Image Vaquita;
    public Image Bird;
    public Image Manatee;
    public bool chosen = false;
    public GameObject NextButton;
    public GameObject StoryText;
    public GameObject PlayAgain;
    public GameObject PlayAgainPerfect;

    bool choosing = false;
    Scenario currentScenario;

    void Start()
    {
        sentences = new Queue<string>();
        readScenario(NextScenario);
    }

    void readScenario(Scenario scenario)
    {
        choosing = false;

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

    public void DisplayNextSentence()
    {
        if (sentences.Count != 0)
        { 
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        else if (!choosing)
        {
            createButtons();
        }
        else
        {
            readScenario(NextScenario);
        }
    }

    void endDialogue()
    {
        Debug.Log("Scenario Finished");
        createButtons();
    }

    void createButtons()
    {
        if (currentScenario.Choices.Count == 0)
        {
            readScenario(currentScenario.DefaultScenario);
        }
        else
        {
            choosing = true;
            for (int i = 0; i < currentScenario.Choices.Count; i++)
            {
                if (currentScenario.Choices[i].Selected)
                {
                    continue;
                }
                Button newButton = Instantiate(ChoiceButton, ButtonLayer.transform);
                newButton.GetComponentInChildren<Text>().text = currentScenario.Choices[i].ButtonText;

                int index = i;
                newButton.onClick.AddListener(delegate { readChoice(currentScenario.Choices[index]); });
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
