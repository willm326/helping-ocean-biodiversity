using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : NewDialogueManager
{
    [SerializeField]
    [Tooltip("Scenes to show the player at the end based on the animals they interacted with")]
    List<AnimalScenario> animalScenarios;

    [SerializeField]
    Image endingImage;

    //The image to be shown to the player based on their moral score
    [Header("Reward Images")]
    [SerializeField]
    Sprite perfect;
    [SerializeField]
    Sprite good;
    [SerializeField]
    Sprite neutral;
    [SerializeField]
    Sprite bad;
    [SerializeField]
    Sprite terrible;

    [SerializeField]
    GameObject animalGrid;
    [SerializeField]
    Image animalImageTemplate;

    Dictionary<Scenario.Animals, Sprite> animalImages;

    new protected void Start()
    {
        animalImages = new Dictionary<Scenario.Animals, Sprite>()
    {
        { Scenario.Animals.Bird, Resources.Load<Sprite>("bird") },
        { Scenario.Animals.Fish, Resources.Load<Sprite>("fish") },
        { Scenario.Animals.Manatee, Resources.Load<Sprite>("manatee") },
        { Scenario.Animals.MonkSeal, Resources.Load<Sprite>("monk-seal") },
        { Scenario.Animals.Shark, Resources.Load<Sprite>("shark") },
        { Scenario.Animals.Turtle, Resources.Load<Sprite>("turtle") },
        { Scenario.Animals.Vaquita, Resources.Load<Sprite>("vaquita") },
    };
    }

    public void readEnding(int accumulatedMorals, Scenario.Animals saved, Scenario.Animals hurt, bool noBadDecisions, bool noGoodDecisions)
    {
        savedAnimals = saved;
        hurtAnimals = hurt;
        morals = accumulatedMorals;
        this.noBadDecisions = noBadDecisions;
        this.noGoodDecisions = noGoodDecisions;
        connectNextButton();
        readScenario(null);
    }

    override protected void readScenario(Scenario scenario)
    {
        if (scenario != null)
        {
            base.readScenario(scenario);
        }    
        else if (animalScenarios.Count > 0)
        {
            AnimalScenario nextAnimal = animalScenarios[0];
            animalScenarios.RemoveAt(0);

            bool animalSaved = (savedAnimals & nextAnimal.AssociatedAnimal) == nextAnimal.AssociatedAnimal;
            bool animalHurt = (hurtAnimals & nextAnimal.AssociatedAnimal) == nextAnimal.AssociatedAnimal;

            if (animalSaved && animalHurt)
            {
                base.readScenario(nextAnimal.ComplexScenario);
            }
            else if (animalSaved)
            {
                base.readScenario(nextAnimal.PositiveScenario);
            }
            else if (animalHurt)
            {
                base.readScenario(nextAnimal.NegativeScenario);
            }
            else
            {
                base.readScenario(nextAnimal.NeutralScenario);
            }
        }
        else
        {
            displayScore();
        }
    }

    void displayScore()
    {
        int savedAnimalsCount = countBits((uint)savedAnimals);

        endingImage.gameObject.SetActive(true);
        if (morals == 0)
        {
            endingImage.sprite = neutral;
        }
        else if (morals > 0)
        {
            if (noBadDecisions && savedAnimalsCount >= 4)
            {
                endingImage.sprite = perfect;
            }
            else
            {
                endingImage.sprite = good;
            }
        }
        else
        {
            if (noGoodDecisions)
            {
                endingImage.sprite = terrible;
            }
            else
            {
                endingImage.sprite = bad;
            }
        }

        foreach (Scenario.Animals animal in animalImages.Keys)
        {
            if ((savedAnimals & animal) == animal)
            {
                Image animalImage = Instantiate(animalImageTemplate, animalGrid.transform);
                animalImage.sprite = animalImages[animal];
            }
        }
    }

    int countBits(uint bitString)
    {
        int count = 0;
        while (bitString > 0)
        {
            if ((bitString & 1) == 1)
            {
                count++;
            }
            bitString >>= 1;
        }
        return count;
    }
}