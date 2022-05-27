using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ss : MonoBehaviour
{
    public Volume Volume;
    bool Cern_Bel = false;
    VolumeProfile profile;
    void Start()
    {
        Volume = GetComponent<Volume>();
        profile = Volume.sharedProfile;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            //fog = profile.Add<>(false);
           

        }
    }
}
