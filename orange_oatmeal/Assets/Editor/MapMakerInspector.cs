using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MapMaker))]
public class MapMakerInspector : Editor
{
    MapMaker myMapMaker;
    bool createRooms = false;

    private void OnSceneGUI()
    {
        if(myMapMaker == null)
        {
            myMapMaker = (MapMaker)target;
        }
               
        if (createRooms)
        {
            Event e = Event.current;
            if(e.type == EventType.MouseUp)
            {
                if(e.button == 0 && e.alt == true)
                {
                    Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        myMapMaker.createRoom(new Vector3(hit.point.x, 0, hit.point.z));

                        EditorUtility.SetDirty(myMapMaker);
                        EditorUtility.SetDirty(myMapMaker.gameObject);
                    }
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal("box");


        if (GUILayout.Button("Create Room Object On Click = " + createRooms))
        {
            createRooms = !createRooms;
        }
        if (GUILayout.Button("PurgeEmpties"))
        {
            myMapMaker.createdRooms = myMapMaker.createdRooms.Where(item => item != null).ToList();
        }

        GUILayout.EndHorizontal();


        DrawDefaultInspector();



    }
}
