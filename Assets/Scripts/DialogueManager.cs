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

public class DialogueManager : MonoBehaviour
{
    //Stores string values that can be checked by scenarios if desired (like an inventory of available items)
    List<string> conditions = new List<string>();

    public List<string> Conditions { get; }

    public AudioSource Type;
    private Queue<string> sentences = new Queue<string>();
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
    List<Button> buttons = new List<Button>();

    [SerializeField]
    Timer choiceTimer;
    [SerializeField]
    EndingManager endingManager;
    [SerializeField]
    Button nextButton;

    [SerializeField]
    protected Scenario.Animals savedAnimals;
    [SerializeField]
    protected Scenario.Animals hurtAnimals;
    [SerializeField]
    protected int morals = 0;
    protected bool noBadDecisions = true;
    protected bool noGoodDecisions = true;

    protected void Start()
    {
        connectNextButton();
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
        else //state should be DialogueState.ReadingResponse
        {
            readScenario(NextScenario);
        }
    }

    protected virtual void readScenario(Scenario scenario)
    {
        destroyButtons();
        currentState = DialogueState.ReadingScenario;

        if (scenario != null)
        {
            currentScenario = scenario;
        }
        else if (currentScenario.DefaultScenario != null)
        {
            currentScenario = currentScenario.DefaultScenario;
        }
        else
        {
            disconnectNextButton();
            StopAllCoroutines();
            endingManager.readEnding(morals, savedAnimals, hurtAnimals, noBadDecisions, noGoodDecisions);
            return;
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

    protected void connectNextButton()
    {
        nextButton.onClick.AddListener(DisplayNextSentence);
    }

    protected void disconnectNextButton()
    {
        nextButton.onClick.RemoveListener(DisplayNextSentence);
    }

    void readChoice(Choice choice)
    {
        destroyButtons();
        currentState = DialogueState.ReadingResponse;

        morals += choice.MoralValue;
        if (choice.MoralValue > 0)
        {
            noGoodDecisions = false;
        }
        else if (choice.MoralValue < 0)
        {
            noBadDecisions = false;
        }

        foreach (string condition in choice.NewConditions)
        {
            conditions.Add(condition);
        }

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
                    buttons.Add(newButton);
                    newButton.GetComponentInChildren<Text>().text = currentScenario.Choices[i].ButtonText;

                    foreach (string condition in currentScenario.Choices[i].RequiredConditions)
                    {
                        if (!conditions.Contains(condition))
                        {
                            newButton.enabled = false;
                        }
                    }

                    int index = i; //Used to specify index of Choices array. If i is used all of the functions will use the final value of i
                    newButton.onClick.AddListener(() =>
                    {
                        readChoice(currentScenario.Choices[index]);
                    });

                    if (currentScenario.TimeLimit > 0)
                    {
                        newButton.onClick.AddListener(() => choiceTimer.Stop());
                    }
                }
            }

            if (currentScenario.TimeLimit > 0)
            {
                choiceTimer.SetTimer(currentScenario.TimeLimit, delegate { readScenario(currentScenario.DefaultScenario); });
            }
        }
    }

    void destroyButtons()
    {
        foreach (Button button in buttons)
        {
            Destroy(button.gameObject);
        }
        buttons.Clear();
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
