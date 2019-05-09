using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;

    public float speed = .2f;
    public void Start()
    {
        objectToFollow = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.z = Mathf.Lerp(this.transform.position.z, objectToFollow.transform.position.z, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);

        this.transform.position = position;
    }
}
