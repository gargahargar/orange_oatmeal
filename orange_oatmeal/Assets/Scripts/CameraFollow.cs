using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    [SerializeField] float height;

    [SerializeField] float speed = .2f;

    public void Start()
    {
        objectToFollow = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.z = Mathf.Lerp(transform.position.z, objectToFollow.transform.position.z, interpolation);
        position.y = Mathf.Lerp(transform.position.y, objectToFollow.transform.position.y + height, interpolation);
        position.x = Mathf.Lerp(transform.position.x, objectToFollow.transform.position.x, interpolation);

        this.transform.position = position;
    }
}
