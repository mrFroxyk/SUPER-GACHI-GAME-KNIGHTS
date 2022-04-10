using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerapolet : MonoBehaviour
{
    public float speed_rot = 3f;
    public player player;
    public GameObject parent;
    public GameObject cam;

    void Start()
    {
        player = parent.GetComponent<player>();
    }

    void Update()
    {
        if (player.NaZemle())
        {
            Quaternion trans = cam.transform.rotation;
            transform.rotation = new Quaternion(0, trans.y, 0, trans.w);
        }   
    }
}
