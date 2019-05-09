using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    RoomScript rs;
    SpaceScript ss;
    [SerializeField]GameObject CurrentSpace;
    // Start is called before the first frame update
    private void Start()
    {
        ss = CurrentSpace.GetComponent<SpaceScript>();
        rs = ss.transform.parent.GetComponent<RoomScript>();
    }
    public List<string> Move(int i)
    {
        List<string> result = new List<string>();
        ss = CurrentSpace.GetComponent<SpaceScript>();
        if (ss.exits[i] != null)
        {            
            bool enteringNewRoom = false;
            string oldRoomName = rs.GetRoomTitle();

            CurrentSpace = ss.exits[i];
            ss = CurrentSpace.GetComponent<SpaceScript>();
            rs = ss.GetRoomScript();

            // check if old room has same name as new room
            if(!Equals(rs.GetRoomTitle(), oldRoomName))
            {
                enteringNewRoom = true;
            }


            if(enteringNewRoom)
            {
                result.Add(rs.GetRoomTitle());
                result.Add(rs.GetRoomDescription());
            }
            result.Add(ss.GetSpaceTitle());
            result.Add(ss.GetSpaceDescription());

        }
        else
            result.Add("There are no exits that direction.");

        return result;
    }

    public List<string> LookSpace()
    {
        List<string> result = new List<string>();
        result.Add(ss.GetSpaceTitle());
        result.Add(ss.GetSpaceDescription());
        return result;
    }
    public List<string> LookRoom()
    {
        List<string> result = new List<string>();
        result.Add(rs.GetRoomTitle());
        result.Add(rs.GetRoomDescription());
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentSpace != null)
        {
            transform.position = new Vector3(CurrentSpace.transform.position.x, transform.position.y, CurrentSpace.transform.position.z);
        }
    }
}
