using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : Button
{
    [SerializeField]
    int moralValue = 0; //How much this choice should add to the player's "moral" score. Use negative values to take away score instead.
    Dialogue nextScenario; //The next scenario this button leads to.

    void OnButtonPress(int choice)
    {
        WorldVariables.morals += moralValue;

        WorldVariables.choice = choice;
        WorldVariables.choosing = false;
        GameObject[] Buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (GameObject Button in Buttons)
        {
            Destroy(Button.gameObject);
        }
    }
}
