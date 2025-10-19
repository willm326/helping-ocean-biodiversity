using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Convert : MonoBehaviour
{
    
    public void convertObjects()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Scenario");
        foreach (GameObject gameObject in gameObjects)
        {
            Dialogue dialogue = gameObject.GetComponent<Dialogue>();
            Scenario newScenario = ScriptableObject.CreateInstance<Scenario>();
            string path = "Assets/Scenarios/" + convertName(gameObject.name) + ".asset";
            newScenario.Sentences = dialogue.sentences;
            newScenario.Speaker = dialogue.speaker;
            newScenario.Character = dialogue.Character;
            newScenario.Environment = dialogue.Environment;
            newScenario.Overlay = dialogue.Cinematic;

            newScenario.Choices = new Choice[dialogue.Options];
            for (int choice = 0; choice < dialogue.Options; choice++)
            {

            }

            AssetDatabase.CreateAsset(newScenario, path);
        }
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
            else if (letter != '(' && letter != ')')
            {
                newString += letter;
            }
        }
        return newString;
    }
}
