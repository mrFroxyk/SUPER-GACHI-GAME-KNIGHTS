using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax : MonoBehaviour
{
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float parallaxStrength = 0.1f;
    [SerializeField] bool disableVerticalParallax;
    Vector3 targetPreviousPosition;
    void Start()
    {
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;
            
        }
        targetPreviousPosition = followingTarget.position;
        Invoke("sc", 0.2f);
    }
    void Update()
    {
        Vector3 delta = followingTarget.position - targetPreviousPosition;
        if (disableVerticalParallax)
        {
            delta.y = 0;
        }
        
        targetPreviousPosition = followingTarget.position;
        transform.position += delta * parallaxStrength;
    }
    void sc()
    {
        disableVerticalParallax = false;
    }
}




