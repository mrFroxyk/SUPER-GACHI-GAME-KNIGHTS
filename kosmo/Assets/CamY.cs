using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamY : MonoBehaviour
{
    public float speed_rot = 3f;
    public GameObject cam;

    void Start()
    {
    }

    void Update()
    {

            Quaternion trans = cam.transform.rotation;
            transform.rotation = new Quaternion(0, trans.y, 0, trans.w);
        
    }
}
