using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] SpaceScript currentSpaceScript;
    [SerializeField] GameObject currentSpace;

    // Start is called before the first frame update
    void Start()
    {
        if (currentSpaceScript == null)
        {
            currentSpaceScript = currentSpace.GetComponent<SpaceScript>();
        }
        if(!currentSpaceScript.GetResidents().Contains(gameObject))
            currentSpaceScript.AddMeToResidents(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
   //     if (!currentSpaceScript.GetResidents().Contains(gameObject))
    //        currentSpaceScript.AddMeToResidents(gameObject);
    }
}
