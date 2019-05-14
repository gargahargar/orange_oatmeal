using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class AddExitsWindow : EditorWindow
{
    public GameObject Room1;
    public GameObject Room2;

    public GameObject holder1;
    public GameObject holder2;

    [MenuItem("Window/Add Exits Tool")]
    public static void ShowWindow()
    {
        GetWindow<AddExitsWindow>("Add Exits Tool");
    }



    private void OnGUI()
    {
        GUILayout.Label("First select the first room, then select the second room. Then pick\nthe button that represents the desired exit direction from the first\nroom to the second room.");
        GUILayout.Label("The exit will be automatically reciprocated from room2 to room1.");
       
        string[] names = Enum.GetNames(typeof(Exit));
        Exit[] values = (Exit[])Enum.GetValues(typeof(Exit));

        if (GUILayout.Button("Set Room One"))
        {
            if (Selection.gameObjects.Length == 1)
            {
                Room1 = Selection.gameObjects[0];
                if (holder1 != null)
                    DestroyImmediate(holder1);
                holder1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                holder1.transform.position = Room1.transform.position;
            }
        }
        if (GUILayout.Button("Set Room Two"))
        {
            if (Selection.gameObjects.Length == 1)
            {
                Room2 = Selection.gameObjects[0];
                if (holder2 != null)
                    DestroyImmediate(holder2);
                holder2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                holder2.transform.position = Room2.transform.position;
            }
        }

        GUILayout.BeginHorizontal("box");
        for (int j = 0; j < names.Length; j++)
        {
            if (GUILayout.Button(names[j]))
            {
                if(Room1 != null && Room2 != null)
                {
                    Room1.GetComponent<SpaceScript>().SetExit(Room2, values[j]);
                    Room1.GetComponent<SpaceScript>().DrawExitLine(Room2, j);
                    Reset();
                }
            }
        }
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Reset"))
        {
            Reset();
        }
    }
    private void Reset()
    {
        Room1 = null;
        Room2 = null;

        if (holder1 != null)
            DestroyImmediate(holder1);

        if (holder2 != null)
            DestroyImmediate(holder2);
    }
}


