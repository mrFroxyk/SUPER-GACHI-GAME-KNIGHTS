using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        if (!player)
        {
            player = FindObjectOfType<move>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        pos = player.position;
        pos.z = -10f;
        pos.y += 2f;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime*4);
        //Debug.Log(transform.position- pos);
    }
}
