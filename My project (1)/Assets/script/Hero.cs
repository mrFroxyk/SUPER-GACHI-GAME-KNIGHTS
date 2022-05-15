using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : sushnosti
{

    public static Hero Instance { get; set; }
    public int hp=4;
    protected SpriteRenderer SpriteRenderer;
    private bool IsCanPolucitDamage = true;
    public float z = 0.8f;
    public void Awake()
    {
        Instance = this;
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(IsCanPolucitDamage);
    }
    public override void hit()
    {
        if (IsCanPolucitDamage)
        {
            SpriteRenderer.color = Color.red;
            StartCoroutine(cvet());
            Invoke("Pere", z);
            hp -= 1;
            Debug.Log(hp);
        }
        
        if (hp == 0)
        {
            die();
        }
    }
    IEnumerator cvet()
    {
        
        for (float i = 0f; i < z; i += Time.deltaTime)
        {
            //Debug.Log(Color.Lerp(Color.red, Color.white, z - i));
            SpriteRenderer.color = Color.Lerp(Color.white, Color.red, z - i);
            IsCanPolucitDamage = false;

            yield return null;
        }
    }
    void Pere()  //перезарядка звука
    {
        IsCanPolucitDamage = true;
    }
}
