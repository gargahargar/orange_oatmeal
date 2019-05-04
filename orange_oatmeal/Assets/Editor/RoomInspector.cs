using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(RoomScript))]
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
        //GUILayout.BeginHorizontal("box");
        foreach (TerrainType terrain in Enum.GetValues(typeof(TerrainType)))
        {
            if (GUILayout.Button(terrain.ToString()))
            {
                myRoomScript.setRoomTerrainType(terrain);
            }
        }

        DrawDefaultInspector();
    }
}
