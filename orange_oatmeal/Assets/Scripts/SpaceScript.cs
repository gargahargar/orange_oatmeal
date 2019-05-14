using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpaceScript : MonoBehaviour
{
    // Exit reference sheet
    // 0  1   2  3   4   5   6  7   8   9   10   11 
    // N  NE  E  SE  IN  UP  S  SW  W   NW  OUT  DOWN
    // Opposite exits are 6 positions apart
    [SerializeField]
    GameObject exitLine;

    public GameObject[] exits;
    public GameObject[] exitLines;
    [SerializeField]
    string spaceTitle;
    [SerializeField]
    string spaceDescription;

    [SerializeField] List<GameObject> residents;

    RoomScript rs;
    
    private void Start()
    {
        bool needsScanned = true;
        foreach(GameObject go in exits)
        {
            if(go != null)
                needsScanned = false;
        }
        if (needsScanned)
        {
            exits = new GameObject[12];
            exitLines = new GameObject[12];

            ScanForExits();

            for (int i = 0; i < exits.Length; i++)
                DrawExitLine(exits[i], i);

            rs = transform.parent.gameObject.GetComponent<RoomScript>();
        }

        if (spaceDescription == "")
        {
            spaceDescription = "NO DESCRIPTION FOR THIS SPACE";
        }
        if (spaceTitle == "")
        {
            spaceTitle = "SPACE";
        }
    }
    public string GetSpaceDescription()
    {
        return spaceDescription;
    }
    public string GetSpaceTitle()
    {
        return spaceTitle;
    }
    public RoomScript GetRoomScript()
    {
        if(rs == null)
            rs = transform.parent.gameObject.GetComponent<RoomScript>();
        return rs;
    }
    
    public void DrawExitLine(GameObject g, int position)
    {        
        if(!(g == null))
        {
            GameObject myLine = Instantiate(exitLine);
            myLine.transform.position = transform.position;

            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, g.transform.position);
            myLine.transform.parent = transform;
            exitLines[position] = myLine;
        }
        
    }
    public void AddMeToResidents(GameObject newResident)
    {
        residents.Add(newResident);
    }
    public void RecalculateExitsFromExistingLines()
    {
        for(int i = 0; i < exits.Length; i++)
        {
            if (exitLines[i] == null)
                exits[i] = null;
        }
    }
    public void RemoveThisExitLineFromSpace(GameObject el)
    {
        for (int i = 0; i < exits.Length; i++)
        {
            if (exitLines[i] == el && exits[i] )
            {
                exits[i].GetComponent<SpaceScript>().exits[(i + 6) % 12] = null;
                exits[i] = null;
            }
        }

    }
    public void DeleteAllExitLines()
    {
        for(int i = 0; i < exitLines.Length; i++)
        {
            if(exitLines[i] != null)
                DestroyImmediate(exitLines[i]);
        }
    }
    public void RedrawExitLines()
    {
        for(int i = 0; i < exits.Length; i++)
        {
            if(exits[i] != null)
            {
                DrawExitLine(exits[i], i);
            }
        }
    }
    public List<GameObject> GetResidents()
    {
        return residents;
    }
    public void ScanForExits()
    {
        GameObject[] foundexits = new GameObject[12];
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(0, 0, 1), out hit, 2f))
            SetExit(hit.transform.gameObject, Exit.N);
        if (Physics.Raycast(transform.position, new Vector3(1, 0, 1), out hit, 2f))
            SetExit(hit.transform.gameObject, Exit.NE);
        if (Physics.Raycast(transform.position, new Vector3(1, 0, 0), out hit, 2f))
            SetExit(hit.transform.gameObject, Exit.E);
        if (Physics.Raycast(transform.position, new Vector3(1, 0, -1), out hit, 2f))
            SetExit(hit.transform.gameObject, Exit.SE);
        if (Physics.Raycast(transform.position, new Vector3(0, 1, 0), out hit, 1.5f))
            SetExit(hit.transform.gameObject, Exit.U);
        if (Physics.Raycast(transform.position, new Vector3(0, 0, -1), out hit, 2f))
            SetExit(hit.transform.gameObject, Exit.S);
        if (Physics.Raycast(transform.position, new Vector3(-1, 0, -1), out hit, 2f))
            SetExit(hit.transform.gameObject, Exit.SW);
        if (Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out hit, 2f))
            SetExit(hit.transform.gameObject, Exit.W);
        if (Physics.Raycast(transform.position, new Vector3(-1, 0, 1), out hit, 2f))
            SetExit(hit.transform.gameObject, Exit.NW);
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, 1.5f))
            SetExit(hit.transform.gameObject, Exit.D);
    }
    public void SetExit(GameObject go, Exit e)
    {
        if (go.tag != "Space")
            return;
        exits[(int)e] = go;
        go.GetComponent<SpaceScript>().exits[((int)e + 6) % 12] = this.gameObject;

    }
    private void OnDestroy()
    {
        RemoveThisSpaceFromAllOtherSpaceExits();
        foreach (GameObject go in exitLines)
        {
            if(go != null)
                DestroyImmediate(go);
        }
           
    }

    public void RemoveThisSpaceFromAllOtherSpaceExits()
    {
        for (int i = 0; i < exits.Length; i++)
        {
            if (exits[i] != null)
            {
                DestroyImmediate(exits[i].GetComponent<SpaceScript>().exitLines[(i + 6) % 12]);
                //exits[i].GetComponent<SpaceScript>().exits[(i + 6) % 12] = null;
            }
        }
    }
    
}
public enum Exit
{
    N, NE, E, SE, I, U, S, SW, W, NW, O, D
}