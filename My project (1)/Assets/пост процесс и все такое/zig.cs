using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental;
using UnityEngine.U2D;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Experimental.Rendering.Universal;
public class zig : MonoBehaviour
{
    public bool z = true;
    public UnityEngine.Experimental.Rendering.Universal.Light2D lg;
    public UnityEngine.Experimental.Rendering.Universal.Light2D lgP;

    private void Awake()
    {
        sc(z);
    }

    // Start is called before the first frame update
    void Start()
    {

        //Light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            z = !z;
            sc(z);
        }
    }
    void sc(bool z)
    {
        if (z)
        {
            lg.intensity = 0.84f;
            lgP.intensity = 0;
        }
        else
        {
            lg.intensity = 0;
            lgP.intensity = 0.91f;
        }
        
    }
}
