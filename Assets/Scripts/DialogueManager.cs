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

    private Scenario currentScenario;
    public Scenario NextScenario;
    public Button ChoiceButton;
    public GameObject ButtonLayer;

    [SerializeField]
    DialogueBox dialogueBox;

    DialogueState currentState = DialogueState.ReadingScenario;
    List<Button> buttons = new List<Button>();

    [SerializeField]
    Timer choiceTimer;
    [SerializeField]
    EndingManager endingManager;

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
        dialogueBox.QueueIsEmpty += dialogueFinished;
        readScenario(NextScenario);
    }

    void dialogueFinished(object sender, System.EventArgs e)
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

    void readScenario(Scenario scenario)
    {
        destroyButtons();
        currentState = DialogueState.ReadingScenario;

        if (scenario != null)
        {
            currentScenario = scenario;
            dialogueBox.ReadScenario(scenario);
        }
        else if (currentScenario.DefaultScenario != null)
        {
            currentScenario = currentScenario.DefaultScenario;
            dialogueBox.ReadScenario(scenario);
        }
        else
        {
            endingManager.readEnding(morals, savedAnimals, hurtAnimals, noBadDecisions, noGoodDecisions);
        }
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

        foreach (string sentence in choice.ResponseSentences)
        {
            dialogueBox.EnqueueSentence(sentence);
        }

        //choice.Selected = true;

        dialogueBox.DisplayNextSentence();
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
}
