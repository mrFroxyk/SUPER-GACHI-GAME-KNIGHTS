using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vrashalka : MonoBehaviour
{
    public float Speed = 1f;
    void Update()
    {
        Vector3 vector3 = new Vector3(0, 45, 0) * Time.deltaTime * Speed;
        vector3 = transform.TransformDirection(vector3);
        transform.Rotate(vector3);
    }
}