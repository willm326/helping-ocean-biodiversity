using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tests : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        findDeadEnds();
    }

    // Checks all Scenarios for any "Dead Ends", places where there is no scenario for the player to visit next
    // As a failsafe the player will be sent to the ending if they encounter a dead end to prevent a softlock
    // These dead ends are likely mistakes unless they are in ending scenarios, since dead ends are used to mark when the next ending scenario should be read
    void findDeadEnds()
    {
        string[] assetNumbers = AssetDatabase.FindAssets("t:Scenario", new[] { "Assets/Scenarios" });
        foreach (string assetNumber in assetNumbers)
        {
            string path = AssetDatabase.GUIDToAssetPath(assetNumber);
            if (path != null)
            {
                Scenario scenario = AssetDatabase.LoadAssetAtPath<Scenario>(path);

                if (scenario.Choices.Count == 0)
                {
                    if (scenario.DefaultScenario == null)
                    {
                        if(path.Contains("Ending"))
                        {
                            Debug.LogWarning("Found deadend at Scenario " + scenario.name + ", but it's an ending scene");
                        }
                        else
                        {
                            Debug.LogError("Found deadend at Scenario " + scenario.name + ", missing default scenario");
                        }
                    }
                }

                for (int i = 0; i < scenario.Choices.Count; i++)
                {
                    if (scenario.Choices[i].NextScenario == null)
                    {
                        if (scenario.DefaultScenario == null)
                        {
                            Debug.LogError("Found deadend at Scenario " + scenario.name + ", Choice: " + i);
                        }
                        else
                        {
                            Debug.LogWarning("Found deadend at Scenario " + scenario.name + ", Choice: " + i + ", but there is a default scenario");
                        }
                    }
                }

            }
        }
    }
}
