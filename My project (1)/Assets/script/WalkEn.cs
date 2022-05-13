using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEn : vrag
{
    private SpriteRenderer sprite;
    private Vector3 dir;
    public float speed = 3f;
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        dir = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    void move()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f, 0.1f);
        if (collider2Ds.Length > 0) { dir *= -1f; }
        transform.position = Vector3.MoveTowards(transform.position, dir+transform.position, Time.deltaTime);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject == Controler.Instance.gameObject)
    //    {

    //    }
    //}
}
