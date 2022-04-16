using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;


public class CentreEcranaVectorLuc : MonoBehaviour
{
    public TimelineClip tim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.red, 3f);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "knopka" && (Input.GetMouseButtonDown(0)))
            {
                Debug.Log("fff");
                
            }
        }
    }
}
