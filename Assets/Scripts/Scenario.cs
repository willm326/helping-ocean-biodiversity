using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Scenario : ScriptableObject
{
    [SerializeField]
    [TextArea(3, 10)]
    [Tooltip("Sentences to read")]
    string[] sentences;

    public string[] Sentences
    {
        get => sentences;
    }

    [SerializeField]
    [Tooltip("List of possible choices for this scenario. If none leave empty")]
    Choice[] choices;

    public Choice[] Choices
    {
        get => choices;
    }

    [SerializeField]
    [Tooltip("The next scenario to read if there are no choices or the choices don't provide a new scenario")]
    Scenario defaultScenario;

    public Scenario DefaultScenario
    {
        get => defaultScenario;
    }
}

[System.Serializable]
public class Choice
{

    [SerializeField]
    [Tooltip("How much this choice should add to the player's moral score. Use negative values to take away score instead")]
    int moralValue;

    public int MoralValue
    {
        get => moralValue;
    }

    [SerializeField]
    [Tooltip("The next scenario this button leads to")]
    Scenario nextScenario;

    public Scenario NextScenario
    {
        get => nextScenario;
    }

    [SerializeField]
    [TextArea(3, 10)]
    [Tooltip("Sentences to read after selecting this")]
    string[] responseSentences;

    public string[] ResponseSentences
    {
        get => responseSentences;
    }
}
