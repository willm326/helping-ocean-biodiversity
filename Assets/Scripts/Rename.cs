using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Rename : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rename();
        AssetDatabase.SaveAssets();
    }

   void rename()
   {
        string[] assetNumbers = AssetDatabase.FindAssets("Scenario", new[] { "Assets/Scenarios" });
        foreach (string assetNumber in assetNumbers)
        {
            string path = AssetDatabase.GUIDToAssetPath(assetNumber);
            if (path != null)
            {
                Scenario scenario = AssetDatabase.LoadAssetAtPath<Scenario>(path);
                AssetDatabase.RenameAsset(path, scenario.name.Substring(9));

            }
        }
    }
}
