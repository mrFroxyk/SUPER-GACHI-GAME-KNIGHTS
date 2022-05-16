using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : sushnosti
{

    public static Hero Instance { get; set; }
    public int hp=4;
    protected SpriteRenderer SpriteRenderer;
    private bool IsCanPolucitDamage = true;
    
    public float TimeCvetaAndGivePizdi = 0.8f;
    
    public GameObject[] gm;
    public void Awake()
    {
        Instance = this;
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    public override void hit()
    {
        if (IsCanPolucitDamage)
        {
            SpriteRenderer.color = Color.red;
            StartCoroutine(cvet());
            Invoke("Pere", TimeCvetaAndGivePizdi);
            hp -= 1;
            
            for (int i = 0;  i <= 4-hp; i++)
            {
                //Debug.Log($"hp {hp}     i {i}");
                gm[4-i].GetComponent<Animator>().SetBool("IsDie", true);
            }
            //foreach (GameObject i in gm)
            //{
                
            //}
        }
        
        if (hp == 0)
        {
            die();
        }
    }
    public override void die()
    {
        hp = 5;
        transform.position = Vector3.zero;
        foreach (GameObject i in gm)
        {
            i.GetComponent<Animator>().SetBool("IsDie",false);
        }
    }
    IEnumerator cvet()
    {
        
        for (float i = 0f; i < TimeCvetaAndGivePizdi; i += Time.deltaTime)
        {
            //Debug.Log(Color.Lerp(Color.red, Color.white, z - i));
            SpriteRenderer.color = Color.Lerp(Color.white, Color.red, TimeCvetaAndGivePizdi - i);
            IsCanPolucitDamage = false;

            yield return null;
        }
        
    }
    
    
    void Pere()  //перезарядка звука
    {
        IsCanPolucitDamage = true;
    }
}
