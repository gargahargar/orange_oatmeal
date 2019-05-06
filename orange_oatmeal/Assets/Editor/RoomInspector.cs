using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(RoomScript))]
//[ExecuteInEditMode]
public class RoomInspector : Editor
{
    RoomScript myRoomScript;

    private void OnSceneGUI()
    {
        if (myRoomScript == null)
        {
            myRoomScript = (RoomScript)target;
        }    
    }
    public override void OnInspectorGUI()
    {
        string[] names = Enum.GetNames(typeof(TerrainType));
        TerrainType[] values = (TerrainType[])Enum.GetValues(typeof(TerrainType));

        GUILayout.BeginHorizontal("box");
        for (int j = 1; j < names.Length/2+1; j++)
        {
            if (GUILayout.Button(names[j]))
            {
                myRoomScript.setRoomTerrainType(values[j]);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");
        for (int j = 1+names.Length/2; j < names.Length; j++)
        {
            if (GUILayout.Button(names[j]))
            {
                myRoomScript.setRoomTerrainType(values[j]);
            }
        }
        GUILayout.EndHorizontal();
        
        DrawDefaultInspector();
    }
}
