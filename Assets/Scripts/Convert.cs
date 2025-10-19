using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Convert : MonoBehaviour
{
    private void Start()
    {
        convertObjects();
        Debug.Log("finished");
    }

    public void convertObjects()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Scenario");
        List<Tuple<Scenario, Dialogue>> list = new List<Tuple<Scenario, Dialogue>>();
        foreach (GameObject gameObject in gameObjects)
        {
            Dialogue dialogue = gameObject.GetComponent<Story>().dialogue;
            Scenario newScenario = ScriptableObject.CreateInstance<Scenario>();
            string path = "Assets/Scenarios/" + convertName(gameObject.name) + ".asset";
            newScenario.Sentences = dialogue.sentences;
            newScenario.Speaker = dialogue.speaker;
            newScenario.Character = dialogue.Character;
            newScenario.Environment = dialogue.Environment;
            newScenario.Overlay = dialogue.Cinematic;

            if(dialogue.vaquita == "good")
            {
                newScenario.SavedAnimals |= Scenario.Animals.Vaquita;
            }
            else if(dialogue.vaquita == "bad")
            {
                newScenario.HurtAnimals |= Scenario.Animals.Vaquita;
            }

            if (dialogue.turtle == "first aid")
            {
                newScenario.SavedAnimals |= Scenario.Animals.Turtle;
            }
            else if (dialogue.turtle == "bad")
            {
                newScenario.HurtAnimals |= Scenario.Animals.Turtle;
            }
            else if (dialogue.turtle == "beach")
            {
                newScenario.SavedAnimals |= Scenario.Animals.Turtle;
                newScenario.HurtAnimals |= Scenario.Animals.Turtle;
            }

            if (dialogue.monkSeal == "good")
            {
                newScenario.SavedAnimals |= Scenario.Animals.MonkSeal;
            }
            else if (dialogue.monkSeal == "bad")
            {
                newScenario.HurtAnimals |= Scenario.Animals.MonkSeal;
            }

            if (dialogue.manatee == "good")
            {
                newScenario.SavedAnimals |= Scenario.Animals.Manatee;
            }
            else if (dialogue.manatee == "bad")
            {
                newScenario.HurtAnimals |= Scenario.Animals.Manatee;
            }

            if (dialogue.bird == "good")
            {
                newScenario.SavedAnimals |= Scenario.Animals.Bird;
            }
            else if (dialogue.bird == "bad")
            {
                newScenario.HurtAnimals |= Scenario.Animals.Bird;
            }

            if (dialogue.shark == "good")
            {
                newScenario.SavedAnimals |= Scenario.Animals.Shark;
            }
            else if (dialogue.shark == "bad")
            {
                newScenario.HurtAnimals |= Scenario.Animals.Shark;
            }

            if (dialogue.fish == "good")
            {
                newScenario.SavedAnimals |= Scenario.Animals.Fish;
            }
            else if (dialogue.fish == "bad")
            {
                newScenario.HurtAnimals |= Scenario.Animals.Fish;
            }

            newScenario.Choices = new Choice[dialogue.Options];
            for (int choice = 0; choice < dialogue.Options; choice++)
            {
                newScenario.Choices[choice] = new Choice();
                if (choice == 0)
                {
                    newScenario.Choices[choice].MoralValue = dialogue.moralsA;
                    newScenario.Choices[choice].ResponseSentences = dialogue.sentencesA;
                    newScenario.Choices[choice].ButtonText = dialogue.OptionAButton;
                }
                else if(choice == 1)
                {
                    newScenario.Choices[choice].MoralValue = dialogue.moralsB;
                    newScenario.Choices[choice].ResponseSentences = dialogue.sentencesB;
                    newScenario.Choices[choice].ButtonText = dialogue.OptionBButton;
                }
                else if (choice == 2)
                {
                    newScenario.Choices[choice].MoralValue = dialogue.moralsC;
                    newScenario.Choices[choice].ResponseSentences = dialogue.sentencesC;
                    newScenario.Choices[choice].ButtonText = dialogue.OptionCButton;
                }
                else if (choice == 3)
                {
                    newScenario.Choices[choice].MoralValue = dialogue.moralsD;
                    newScenario.Choices[choice].ResponseSentences = dialogue.sentencesD;
                    newScenario.Choices[choice].ButtonText = dialogue.OptionDButton;
                }
                else if (choice == 4)
                {
                    newScenario.Choices[choice].MoralValue = dialogue.moralsE;
                    newScenario.Choices[choice].ResponseSentences = dialogue.sentencesE;
                    newScenario.Choices[choice].ButtonText = dialogue.OptionEButton;
                }
                else if (choice == 5)
                {
                    newScenario.Choices[choice].MoralValue = dialogue.moralsF;
                    newScenario.Choices[choice].ResponseSentences = dialogue.sentencesF;
                    newScenario.Choices[choice].ButtonText = dialogue.OptionFButton;
                }
            }

            AssetDatabase.CreateAsset(newScenario, path);
            list.Add(new Tuple<Scenario, Dialogue>(newScenario, dialogue));
        }

        addConnections(list);
    }

    public void addConnections(List<Tuple<Scenario, Dialogue>> list)
    {
        foreach (Tuple<Scenario, Dialogue> tuple in list)
        {
            string path = "";
            if (tuple.Item2.nextScenario != "")
            {
                path = "Assets/Scenarios/Scenario-" + tuple.Item2.nextScenario + ".asset";
            }
            else if (tuple.Item2.nextScenarioF != "")
            {
                path = "Assets/Scenarios/Scenario-" + tuple.Item2.nextScenarioF + ".asset";
            }

            if (path != "")
            {
                var asset = AssetDatabase.LoadAssetAtPath(path, typeof(Scenario));
                tuple.Item1.DefaultScenario = (Scenario)asset;     
            }

            for (int choice = 0; choice < tuple.Item2.Options; choice++)
            {
                if (choice == 0)
                {
                    tuple.Item1.Choices[choice].NextScenario = getAsset(tuple.Item2.nextScenarioA);
                }
                else if (choice == 1)
                {
                    tuple.Item1.Choices[choice].NextScenario = getAsset(tuple.Item2.nextScenarioB);
                }
                else if (choice == 2)
                {
                    tuple.Item1.Choices[choice].NextScenario = getAsset(tuple.Item2.nextScenarioC);
                }
                else if (choice == 3)
                {
                    tuple.Item1.Choices[choice].NextScenario = getAsset(tuple.Item2.nextScenarioD);
                }
                else if (choice == 4)
                {
                    tuple.Item1.Choices[choice].NextScenario = getAsset(tuple.Item2.nextScenarioE);
                }
                else if (choice == 5)
                {
                    tuple.Item1.Choices[choice].NextScenario = getAsset(tuple.Item2.nextScenarioF);
                }
            }
        }
    }

    Scenario getAsset(string scenario)
    {
        string path = "";
        if (scenario != "")
        {
            path = "Assets/Scenarios/Scenario-" + scenario + ".asset";
            var asset = AssetDatabase.LoadAssetAtPath(path, typeof(Scenario));
            return (Scenario)asset;
        }

        return null;
    }

    //Converts the provided string to a proper file name following naming guidelines
    string convertName(string name)
    {
        string newString = "";
        foreach (char letter in name)
        {
            if (letter == ' ')
            {
                newString += '-';
            }
            else if (letter == '/')
            {
                break;
            }
            else if (letter != '(' && letter != ')')
            {
                newString += letter;
            }
        }
        return newString;
    }
}
