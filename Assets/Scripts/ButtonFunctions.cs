using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void SelectOption(int choice)
  {
    
  }


    public void OptionA()
    {
        WorldVariables.choice = 1;
        WorldVariables.choosing = false;
        GameObject[] Buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (GameObject Button in Buttons)
        {
            Destroy(Button.gameObject);
        }
    }

    public void OptionB()
    {
        WorldVariables.choice = 2;
        WorldVariables.choosing = false;
        GameObject[] Buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (GameObject Button in Buttons)
        {
            Destroy(Button.gameObject);
        }
    }

    public void OptionC()
    {
        WorldVariables.choice = 3;
        WorldVariables.choosing = false;
        GameObject[] Buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (GameObject Button in Buttons)
        {
            Destroy(Button.gameObject);
        }
    }

    public void OptionD()
    {
        WorldVariables.choice = 4;
        WorldVariables.choosing = false;
        GameObject[] Buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (GameObject Button in Buttons)
        {
            Destroy(Button.gameObject);
        }
    }

    public void OptionE()
    {
        WorldVariables.choice = 5;
        WorldVariables.choosing = false;
        GameObject[] Buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (GameObject Button in Buttons)
        {
            Destroy(Button.gameObject);
        }
    }

    public void OptionF()
    {
        WorldVariables.choice = 6;
        WorldVariables.choosing = false;
        GameObject[] Buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (GameObject Button in Buttons)
        {
            Destroy(Button.gameObject);
        }
    }

    public void Continue()
    {
        if (!WorldVariables.choosing)
        {
            if (WorldVariables.choice == 0)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }

            else if (WorldVariables.choice == 1)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentenceA();
            }

            else if (WorldVariables.choice == 2)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentenceB();
            }

            else if (WorldVariables.choice == 3)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentenceC();
            }

            else if (WorldVariables.choice == 4)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentenceD();
            }

            else if (WorldVariables.choice == 5)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentenceE();
            }

            else if (WorldVariables.choice == 6)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentenceF();
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }
}
