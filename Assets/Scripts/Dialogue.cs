using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public string Scenario;
    public string nextScenario;
    public string nextScenarioA;
    public string nextScenarioB;
    public string nextScenarioC;
    public string nextScenarioD;
    public string nextScenarioE;
    public string nextScenarioF;
    public string speaker;
    public Sprite Environment;
    public Sprite Character;
    public Sprite Cinematic;
    public int Options;
    public string OptionAButton;
    public string OptionBButton;
    public string OptionCButton;
    public string OptionDButton;
    public string OptionEButton;
    public string OptionFButton;
    public int moralsA = 0;
    public int moralsB = 0;
    public int moralsC = 0;
    public int moralsD = 0;
    public int moralsE = 0;
    public int moralsF = 0;
    public string vaquita = " ";
    public string turtle = " ";
    public string monkSeal = " ";
    public string manatee = " ";
    public string bird = "";
    public string shark = "";
    public string fish = "";
    public float timeLimit = 0;
    public bool timed = false;

    [TextArea(3, 10)]
    public string[] sentences;

    [TextArea(3, 10)]
    public string[] sentencesA;

    [TextArea(3, 10)]
    public string[] sentencesB;

    [TextArea(3, 10)]
    public string[] sentencesC;

    [TextArea(3, 10)]
    public string[] sentencesD;

    [TextArea(3, 10)]
    public string[] sentencesE;

    [TextArea(3, 10)]
    public string[] sentencesF;

}
