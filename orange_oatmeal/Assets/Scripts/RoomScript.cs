using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoomScript : MonoBehaviour
{
    private Renderer rend;
    private string roomName;
    private string roomDescription;
    [SerializeField]
    private TerrainType terrain;

    List<Color> terrainColors;

    public void Start()
    {
        rend = GetComponent<Renderer>();

        terrainColors = new List<Color>();
        terrainColors.Add(Color.white);
        terrainColors.Add(Color.green);
        terrainColors.Add(Color.blue);
        terrainColors.Add(Color.red);
        terrainColors.Add(Color.yellow);
        terrainColors.Add(Color.grey);
        terrainColors.Add(Color.black);
        terrainColors.Add(Color.cyan);
        terrainColors.Add(Color.magenta);
    }
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
        Material tempMaterial = new Material(rend.sharedMaterial);
        tempMaterial.color = terrainColors[(int)t];
        rend.sharedMaterial = tempMaterial;
    }
    public TerrainType getRoomTerrainType()
    {
        return terrain;
    }
}
public enum TerrainType
{
    none,
    forest,
    water,
    mountain,
    desert,
    plains,
    dungeon,
    house,
    castle
}
