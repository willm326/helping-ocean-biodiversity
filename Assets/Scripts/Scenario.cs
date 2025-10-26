using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu]
public class Scenario : ScriptableObject
{
    [Flags]
    public enum Animals
    {
        None = 0,
        Vaquita = 1,
        Turtle = 2,
        MonkSeal = 4,
        Manatee = 8,
        Bird = 16,
        Shark = 32,
        Fish = 64
    }

    [SerializeField]
    [TextArea(3, 10)]
    [Tooltip("Sentences to read")]
    string[] sentences;
    public string[] Sentences
    {
        get => sentences;
    }

    [SerializeField]
    string speaker;
    public string Speaker
    {
        get => speaker;
    }

    [SerializeField]
    Sprite character;
    public Sprite Character
    {
        get => character;
    }

    [SerializeField]
    Sprite environment;
    public Sprite Environment
    {
        get => environment;
    }

    [SerializeField]
    [Tooltip("Extra sprite that is added on top of the environemnt and character sprites")]
    Sprite overlay;
    public Sprite Overlay
    {
        get => overlay;
    }

    [SerializeField]
    [Tooltip("List of possible choices for this scenario. If none leave empty")]
    List<Choice> choices;
    public List<Choice> Choices
    {
        get => choices;
    }

    [SerializeField]
    [Tooltip("The animals that were saved in this scenario")]
    Animals savedAnimals;
    public Animals SavedAnimals
    {
        get => savedAnimals;
    }

    [SerializeField]
    [Tooltip("The animals that were hurt by this interaction")]
    Animals hurtAnimals;
    public Animals HurtAnimals
    {
        get => hurtAnimals;
    }

    [SerializeField]
    [Tooltip("The next scenario to read if there are no choices or the choices don't provide a new scenario")]
    Scenario defaultScenario;
    public Scenario DefaultScenario
    {
        get => defaultScenario;
    }

    [SerializeField]
    [Tooltip("The amount of time (in seconds) the player has to select an option before the default is selected")]
    float timeLimit;
    public float TimeLimit
    {
        get => timeLimit;
    }
}

[System.Serializable]
public class Choice
{
    [SerializeField]
    string buttonText;
    public string ButtonText
    {
        get => buttonText;
    }

    [SerializeField]
    [Tooltip("How much this choice should add to the player's moral score. Use negative values to take away score instead")]
    int moralValue;
    public int MoralValue
    {
        get => moralValue;
    }

    [SerializeField]
    [Tooltip("The next scenario this button leads to")]
    Scenario[] nextScenarios = new Scenario[1]; //Is an Array to allow one scenario to lead to multiple outcomes (chosen randomly)
    public Scenario NextScenario
    {
        get
        {
            if (nextScenarios.Length == 0)
            {
                return null;
            }
            else if (nextScenarios.Length == 1)
            {
                return nextScenarios[0];
            }
            else
            {
                int selection = UnityEngine.Random.Range(0, nextScenarios.Length);
                return nextScenarios[selection];
            }
        }
        set => nextScenarios[0] = value;
    }

    [SerializeField]
    [TextArea(3, 10)]
    [Tooltip("Sentences to read after selecting this")]
    string[] responseSentences;
    public string[] ResponseSentences
    {
        get => responseSentences;
    }

    [SerializeField]
    [Tooltip("Conditions the player must have to select this option")]
    string[] requiredConditions;
    public string[] RequiredConditions
    {
        get => requiredConditions;
    }

    [SerializeField]
    [Tooltip("List of conditions to add to the player, such as giving them an item")]
    string[] newConditions;
    public string[] NewConditions
    {
        get => newConditions;
    }

    public bool Selected { get; set; } = false;

    public void OnEnable()
    {
        Selected = false;
    }
}