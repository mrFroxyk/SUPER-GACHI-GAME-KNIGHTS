﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vrag : MonoBehaviour
{
    public virtual void hit()
    {

    }
    public virtual void die()
    {
        Destroy(this.gameObject);
    }

}
