using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{

    public List<GameObject> createdRooms;
    public GameObject roomPrefab;

    public void createRoom(Vector3 pos)
    {
        if(createdRooms == null)
        {
            createdRooms = new List<GameObject>();
        }

        GameObject g = Instantiate(roomPrefab, pos, Quaternion.identity);
        createdRooms.Add(g);
    }
}
