using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ExitLineDelete : MonoBehaviour
{
    // This is called if deleted
    private void OnDestroy()
    {
        SpaceScript ss = transform.parent.gameObject.GetComponent<SpaceScript>();
        ss.RemoveThisExitLineFromSpace(gameObject);
    }
}
