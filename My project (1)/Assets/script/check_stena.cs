using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_stena : MonoBehaviour
{
    public bool Check()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 1f), 0f);
        if (colliders.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
