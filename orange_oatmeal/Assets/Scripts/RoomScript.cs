using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoomScript : MonoBehaviour
{
    private Renderer rend;
    [SerializeField]
    private string roomTitle;
    [SerializeField]
    private string roomDescription;
    [SerializeField]
    private TerrainType terrain;
    int width;
    int height;

    List<Color> terrainColors;

    public void Start()
    {
        
        width = (int)transform.localScale.x;
        height = (int)transform.localScale.z;

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

        if (roomTitle == "")
            roomTitle = "ROOM";
        if (roomDescription == "")
            roomDescription = "NO DESC FOR THIS ROOM";
    }
    public void setRoomTitle(string s)
    {
        roomTitle = s;
    }
    public string GetRoomTitle()
    {
        return roomTitle;
    }
    public void setRoomDescription(string s)
    {
        roomDescription = s;
    }
    public string GetRoomDescription()
    {
        return roomDescription;
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
    public void setSize(int w, int h)
    {
        if(w > 1 && h > 1)
        {
            // unparent all children, then scale the transform, then reparent all the children
            List<Transform> tempChildren = new List<Transform>();
            foreach (Transform child in transform)
                tempChildren.Add(child);

            transform.DetachChildren();
            transform.localScale = new Vector3(w, transform.localScale.y, h);
            height = h;
            width = w;
            foreach (Transform t in tempChildren)
                t.parent = this.transform;
        }

    }
    public int getWidth()
    {
        return width;
    }
    public int getHeight()
    {
        return height;
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
