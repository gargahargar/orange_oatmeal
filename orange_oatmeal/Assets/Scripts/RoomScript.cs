using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    private string roomName;
    private string roomDescription;
    [SerializeField]
    private TerrainType terrain;

    public void setRoomName(string s)
    {
        roomName = s;
    }
    public void setRoomDescription(string s)
    {
        roomDescription = s;
    }
    public void setRoomTerrainType(TerrainType t)
    {
        terrain = t;
    }
}
public enum TerrainType
{
    forest,
    water,
    mountain,
    desert,
    plains
}
