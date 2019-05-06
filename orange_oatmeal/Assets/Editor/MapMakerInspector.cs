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
    bool createSpaces = false;

    private void OnSceneGUI()
    {
        if(myMapMaker == null)
        {
            myMapMaker = (MapMaker)target;
        }
               
        if ( createRooms || createSpaces)
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
                        if(hit.collider.gameObject.tag == "Ground" && createRooms)
                        {
                            myMapMaker.CreateRoom(new Vector3(hit.point.x, 0, hit.point.z), hit.collider.gameObject);


                        }else if(hit.collider.gameObject.tag == "Room" && createSpaces)
                        {
                            myMapMaker.CreateSpace(new Vector3(hit.point.x, 0, hit.point.z), hit.collider.gameObject);

                            EditorUtility.SetDirty(myMapMaker);
                            EditorUtility.SetDirty(myMapMaker.gameObject);
                        }
                    }
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal("box");


        if (GUILayout.Button("Create Room = " + createRooms))
        {
            createRooms = !createRooms;
        }
        if (GUILayout.Button("Create Space = " + createSpaces))
        {
            createSpaces = !createSpaces;
        }

        GUILayout.EndHorizontal();
        if (GUILayout.Button("PurgeEmpties"))
        {
            myMapMaker.createdRooms = myMapMaker.createdRooms.Where(item => item != null).ToList();
            myMapMaker.createdSpaces = myMapMaker.createdSpaces.Where(item => item != null).ToList();
        }

        DrawDefaultInspector();



    }
}
