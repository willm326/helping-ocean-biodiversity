using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
class AnimalScenario : ScriptableObject
{
    [SerializeField]
    [Tooltip("Animal this collection of scenes is associated with")]
    Scenario.Animals associatedAnimal;
    public Scenario.Animals AssociatedAnimal
    {
        get => associatedAnimal;
    }

    [SerializeField]
    [Tooltip("Positive scenario to read when the player saved the animal")]
    Scenario positiveScenario;
    public Scenario PositiveScenario
    {
        get => positiveScenario;
    }

    [SerializeField]
    [Tooltip("Negative scenario to read when the player hurt the animal")]
    Scenario negativeScenario;
    public Scenario NegativeScenario
    {
        get => negativeScenario;
    }

    [SerializeField]
    [Tooltip("Neutral scenario to read when the player didn't have a significant impact on the animal")]
    Scenario neutralScenario;
    public Scenario NeutralScenario
    {
        get => neutralScenario;
    }

    [SerializeField]
    [Tooltip("An optional extra scenario to read when both the saved and hurt flag for the animal are checked.")]
    Scenario complexScenario;
    public Scenario ComplexScenario
    {
        get => complexScenario;
    }
}
