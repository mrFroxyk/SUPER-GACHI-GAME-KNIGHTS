using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : vrag
{

    public static Controler Instance { get; set; }
    public int hp=4;
    public void Awake()
    {
        Instance = this;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void hit()
    {
        hp -= 1;
        Debug.Log(hp);
        if (hp == 0)
        {
            die();
        }
    }
}
