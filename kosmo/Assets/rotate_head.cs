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


    // Start is called before the first frame update
    void Start()
    {

    }
    

    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * speed_rot;
        rotationY -= Input.GetAxis("Mouse Y") * speed_rot;
        rotationY = Mathf.Clamp(rotationY, -90f, 75f);
        transform.eulerAngles = new Vector3(rotationY, rotationX, 0f);
        

    }
}
