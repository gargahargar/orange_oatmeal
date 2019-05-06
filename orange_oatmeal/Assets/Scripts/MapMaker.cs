using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{

    public List<GameObject> createdRooms;
    public List<GameObject> createdSpaces;

    public GameObject roomPrefab;
    public GameObject spacePrefab;

    public void Start()
    {
        if (createdRooms == null)
        {
            createdRooms = new List<GameObject>();
        }
        if (createdSpaces == null)
        {
            createdSpaces = new List<GameObject>();
        }
    }

    public void CreateRoom(Vector3 pos, GameObject groundBase)
    {
        pos = GetNearestPosition(pos);

        GameObject g = Instantiate(roomPrefab, pos, Quaternion.identity);
        g.transform.parent = groundBase.transform;
        createdRooms.Add(g);
    }
    public void CreateSpace(Vector3 pos, GameObject roomBase)
    {
        pos = GetNearestPosition(pos);
        pos += new Vector3(0, .2f, 0);

        GameObject g = Instantiate(spacePrefab, pos, Quaternion.identity);
        g.transform.parent = roomBase.transform;
        createdSpaces.Add(g);
    }
    public Vector3 GetNearestPosition(Vector3 old)
    {
        return new Vector3(Mathf.Round(old.x/2)*2, Mathf.Round(old.y), Mathf.Round(old.z/2)*2);
    }
}
