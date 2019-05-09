using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MapMaker))]
public class MapMakerInspector : Editor
{
    MapMaker myMapMaker;
    bool createRooms = true;
    bool createSpaces = true;

    private void OnSceneGUI()
    {
        // This was on the unity tutorial for how to do a basic custom editor script so i didnt change it
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
                            myMapMaker.CreateRoom(new Vector3(hit.point.x, 0, hit.point.z));
                            EditorUtility.SetDirty(myMapMaker);
                            EditorUtility.SetDirty(myMapMaker.gameObject);

                        }
                        else if(hit.collider.gameObject.tag == "Room" && createSpaces)
                        {
                            myMapMaker.CreateSpace(new Vector3(hit.point.x, 0, hit.point.z), hit.collider.gameObject.transform);

                            EditorUtility.SetDirty(myMapMaker);
                            EditorUtility.SetDirty(myMapMaker.gameObject);
                        }else if(hit.collider.gameObject.tag == "Space" && createSpaces)
                        {
                            Transform roomTransform = hit.collider.transform;
                            while(roomTransform.gameObject.tag != "Room")
                                roomTransform = roomTransform.parent.transform;

                            myMapMaker.CreateSpace(hit.point, roomTransform);
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
        EditorGUILayout.BeginHorizontal("box");


        if (GUILayout.Button("Create Room = " + createRooms))
        {
            createRooms = !createRooms;
        }
        if (GUILayout.Button("Create Space = " + createSpaces))
        {
            createSpaces = !createSpaces;
        }

        EditorGUILayout.EndHorizontal();


        if (GUILayout.Button("PurgeEmpties"))
        {
            myMapMaker.createdRooms = myMapMaker.createdRooms.Where(item => item != null).ToList();
            myMapMaker.createdSpaces = myMapMaker.createdSpaces.Where(item => item != null).ToList();
        }

        DrawDefaultInspector();



    }
}
