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
    private Scenario nextScenario;
    public Button ChoiceButton;
    public GameObject ButtonLayer;


    [SerializeField]
    Scenario startingScenario;
    [SerializeField]
    DialogueBox dialogueBox;

    DialogueState currentState = DialogueState.ReadingScenario;
    List<Button> buttons = new List<Button>();

    [SerializeField]
    Timer choiceTimer;
    [SerializeField]
    EndingManager endingManager;

    protected void Start()
    {
        dialogueBox.QueueIsEmpty += dialogueFinished;
        readScenario(startingScenario);
    }

    void dialogueFinished(object sender, System.EventArgs e)
    {
        Debug.Log(currentState);
        if (currentState == DialogueState.ReadingScenario)
        {
            createButtons();
        }
        else //state should be DialogueState.ReadingResponse
        {
            readScenario(nextScenario);
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
            endingManager.SaveAnimal(scenario.SavedAnimals);
            endingManager.HurtAnimal(scenario.HurtAnimals);
        }
        else if (currentScenario.DefaultScenario != null)
        {
            currentScenario = currentScenario.DefaultScenario;
            dialogueBox.ReadScenario(scenario);
            endingManager.SaveAnimal(scenario.SavedAnimals);
            endingManager.HurtAnimal(scenario.HurtAnimals);
        }
        else
        {
            dialogueBox.QueueIsEmpty -= dialogueFinished;
            endingManager.beginEnding();
        }
    }

    void readChoice(Choice choice)
    {
        destroyButtons();
        currentState = DialogueState.ReadingResponse;

        endingManager.AddMorals(choice.MoralValue);

        foreach (string condition in choice.NewConditions)
        {
            conditions.Add(condition);
        }

        nextScenario = choice.NextScenario;

        if (choice.ResponseSentences.Length != 0)
        {
            foreach (string sentence in choice.ResponseSentences)
            {
                dialogueBox.EnqueueSentence(sentence);
            }

            dialogueBox.DisplayNextSentence();
        }
        else
        {
            readScenario(nextScenario);
        }

        //choice.Selected = true;
    }

    void createButtons()
    {
        currentState = DialogueState.MakingChoice;

        if (currentScenario.Choices.Count == 0)
        {
            nextScenario = currentScenario.DefaultScenario;
            readScenario(nextScenario);
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
