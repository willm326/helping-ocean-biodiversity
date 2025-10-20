using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldVariables
{
    public static int choice = 0;
    public static int morals = 0;
    public static bool choosing = false;
    public static string currentScenario = "1";
    public static bool firstAid = false;
    public static bool OptionA5R = true;
    public static bool OptionB5R = true;
    public static bool OptionC5R = true;
    public static bool OptionD5R = true;
    public static bool OptionE5R = true;
    public static bool OptionA13R = true;
    public static bool OptionB13R = true;
    public static bool OptionC13R = true;
    public static bool OptionD13R = true;
    public static bool secludedArea = false;
    public static bool crowdedArea = false;

    public static string vaquita = "";
    public static string turtle = "";
    public static string monkSeal = "";
    public static string manatee = "";
    public static string bird = "";
    public static string shark = "";
    public static string fish = "";

    public static Scenario.Animals animals = new Scenario.Animals();
}
