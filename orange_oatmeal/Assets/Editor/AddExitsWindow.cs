using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class AddExitsWindow : EditorWindow
{
    [MenuItem("Window/Add Exits Tool")]
    public static void ShowWindow()
    {
        GetWindow<AddExitsWindow>("Add Exits Tool");
    }
    private void OnGUI()
    {
        // window code
       
        GUILayout.Label("First select the first room, then select the second room. Then pick\nthe button that represents the desired exit direction from the first\nroom to the second room.");
        GUILayout.Label("The exit will be automatically reciprocated from room2 to room1.");
        GUILayout.Label("Example: Select room1, hold shift, select room2, click the\nbutton 'E' and the tool adds an exit going east from\nroom1, and a west exit back from room2.");

        string[] names = Enum.GetNames(typeof(Exit));
        Exit[] values = (Exit[])Enum.GetValues(typeof(Exit));

        GUILayout.BeginHorizontal("box");
        for (int j = 0; j < names.Length; j++)
        {
            if (GUILayout.Button(names[j]))
            {
                if(Selection.gameObjects.Length == 2)
                {
                    Selection.gameObjects[1].GetComponent<SpaceScript>().SetExit(Selection.gameObjects[0], values[j]);
                    Selection.gameObjects[1].GetComponent<SpaceScript>().DrawExitLine(Selection.gameObjects[0], j);
                }
            }
        }
    }
}
