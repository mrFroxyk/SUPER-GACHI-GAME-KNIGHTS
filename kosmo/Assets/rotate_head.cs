using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_head : MonoBehaviour
{
    public float speed_rot = 3f;
    public player player;
    private float rotationY = 0f;
    private float rotationX = 0f;

    void Start()
    {

    }

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * speed_rot * Time.deltaTime * 50;
        rotationY -= Input.GetAxis("Mouse Y") * speed_rot * Time.deltaTime * 50;
        rotationY = Mathf.Clamp(rotationY, -90f, 75f);
        transform.eulerAngles = new Vector3(rotationY, rotationX, 0f);
    }
}
