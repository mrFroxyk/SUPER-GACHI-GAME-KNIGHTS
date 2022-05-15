using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : sushnosti
{
    [SerializeField] private int hp = 2;
    protected SpriteRenderer SpriteRenderer;
    //public static ememy Instance { get; set; }
    private void Awake()
    {
        //Instance = this;
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public override void hit()
    {
        hp -= 1;
        Debug.Log(hp);
        SpriteRenderer.color = Color.red;
        StartCoroutine(cvet());
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
    IEnumerator cvet()
    {
        float z = 0.8f;
        for (float i = 0f; i < z; i += Time.deltaTime)
        {
            //Debug.Log(Color.Lerp(Color.red, Color.white, z - i));
            SpriteRenderer.color = Color.Lerp(Color.white,Color.red, z - i);        


            yield return null;
        }
    }
}
