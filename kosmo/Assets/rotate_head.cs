using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_head : MonoBehaviour
{
    private float speed_rot = 3f;
    public GameObject chid ;
    public player player;
    private float rotationY = 0f;
    private float rotationX = 0f;

    void Start()
    {

    }

    void Update()
    {

        Rot();

    }
    void Rot()
    {
        rotationX += Input.GetAxis("Mouse X") * 3f * Time.deltaTime * 50;
        rotationY -= Input.GetAxis("Mouse Y") * 3f * Time.deltaTime * 50;

        rotationY = Mathf.Clamp(rotationY, -90f, 75f);
        transform.eulerAngles = new Vector3(rotationY, rotationX, 0f);
    }
}
