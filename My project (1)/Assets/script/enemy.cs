using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : sushnosti
{
    [SerializeField] private int hp = 2;
    protected SpriteRenderer SpriteRenderer;
    public bool IsCanPolucitDamage = true;
    public float TimeNoPolucUron=0.1f;
    public float TimeIzmeneniaCveta = 0.1f;
    //public static ememy Instance { get; set; }
    private void Awake()
    {
        //Instance = this;
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public override void hit()
    {
        if (IsCanPolucitDamage)
        {
            hp -= 1;

            SpriteRenderer.color = Color.red;
            StartCoroutine(IsCanUron());
            StartCoroutine(cvet());
            Invoke("Pere", TimeNoPolucUron+0.05f);
        }

        
        if (hp == 0)
        {
            die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.hit();
        }
        //if (collision.gameObject.tag=="Player")
        //{
        //    Debug.Log(collision.gameObject.GetComponent<Rigidbody2D>());
        //    Debug.Log(collision.gameObject.GetComponent<Controler>());

        //    Controler controler = collision.gameObject.GetComponent<Controler>();

        //}
    }
    IEnumerator IsCanUron()
    {
        for (float i = 0f; i < TimeNoPolucUron; i += Time.deltaTime)
        {

            IsCanPolucitDamage = false;
            yield return null;
        }
    }
    IEnumerator cvet()
    {
        for (float i = 0f; i < TimeIzmeneniaCveta; i += Time.deltaTime)
        {
            //Debug.Log(Color.Lerp(Color.red, Color.white, z - i));
            SpriteRenderer.color = Color.Lerp(Color.white, Color.red, TimeIzmeneniaCveta - i);

            yield return null;
        }
    }
    void Pere()  //перезарядка звука
    {
        IsCanPolucitDamage = true;
    }
}
