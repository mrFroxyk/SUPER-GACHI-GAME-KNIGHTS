using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ememy : vrag
{
    [SerializeField] private int hp = 2;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Controler.Instance.gameObject)
        {
            Controler.Instance.hit();
            hp--;
        }
        if (hp < 1)
        {
            die();
        }
        //if (collision.gameObject.tag=="Player")
        //{
        //    Debug.Log(collision.gameObject.GetComponent<Rigidbody2D>());
        //    Debug.Log(collision.gameObject.GetComponent<Controler>());

        //    Controler controler = collision.gameObject.GetComponent<Controler>();

        //}
    }
}
