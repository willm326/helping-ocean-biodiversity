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
        set => sentences = value;
    }

    [SerializeField]
    string speaker;
    public string Speaker
    {
        get => speaker;
        set => speaker = value;
    }

    [SerializeField]
    Sprite character;
    public Sprite Character
    {
        get => character;
        set => character = value;
    }

    [SerializeField]
    Sprite environment;
    public Sprite Environment
    {
        get => environment;
        set => environment = value;
    }

    [SerializeField]
    [Tooltip("Extra sprite that is added on top of the environemnt and character sprites")]
    Sprite overlay;
    public Sprite Overlay
    {
        get => overlay;
        set => overlay = value;
    }

    [SerializeField]
    [Tooltip("List of possible choices for this scenario. If none leave empty")]
    List<Choice> choices;
    public List<Choice> Choices
    {
        get => choices;
        set => choices = value;
    }

    [SerializeField]
    [Tooltip("The animals that were saved in this scenario")]
    Animals savedAnimals;
    public Animals SavedAnimals
    {
        get => savedAnimals;
        set => savedAnimals = value;
    }

    [SerializeField]
    [Tooltip("The animals that were hurt by this interaction")]
    Animals hurtAnimals;
    public Animals HurtAnimals
    {
        get => hurtAnimals;
        set => hurtAnimals = value;
    }

    [SerializeField]
    [Tooltip("The next scenario to read if there are no choices or the choices don't provide a new scenario")]
    Scenario defaultScenario;
    public Scenario DefaultScenario
    {
        get => defaultScenario;
        set => defaultScenario = value;
    }

    [SerializeField]
    [Tooltip("The amount of time (in seconds) the player has to select an option before the default is selected")]
    float timeLimit;
    public float TimeLimit
    {
        get => timeLimit;
        set => timeLimit = value;
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
        set => buttonText = value;
    }

    [SerializeField]
    [Tooltip("How much this choice should add to the player's moral score. Use negative values to take away score instead")]
    int moralValue;
    public int MoralValue
    {
        get => moralValue;
        set => moralValue = value;
    }

    [SerializeField]
    [Tooltip("The next scenario this button leads to")]
    Scenario nextScenario;
    public Scenario NextScenario
    {
        get => nextScenario;
        set => nextScenario = value;
    }

    [SerializeField]
    [TextArea(3, 10)]
    [Tooltip("Sentences to read after selecting this")]
    string[] responseSentences;
    public string[] ResponseSentences
    {
        get => responseSentences;
        set => responseSentences = value;
    }

    public bool Selected { get; set; } = false;

    public void OnEnable()
    {
        Selected = false;
    }
}