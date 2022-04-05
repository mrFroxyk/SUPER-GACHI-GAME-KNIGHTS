using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_head : MonoBehaviour
{
    private float speed_rot;
    public GameObject chid ;
    public player player;
    public float rotationX = 0f;
    public float rotationY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = chid.GetComponent<player>();
        speed_rot = player.rotate_speed;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    

    // Update is called once per frame
    void Update()
    {
        float speed = 0.5f;
        rot();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 1.5f;
        }

        transform.localPosition += new Vector3(transform.forward.x, 0, transform.forward.z) * Input.GetAxis("Vertical") * speed * 2.5f * Time.deltaTime;
        transform.localPosition += new Vector3(transform.right.x, 0, transform.right.z) * Input.GetAxis("Horizontal") * speed * 2.5f * Time.deltaTime;
        transform.Rotate(-Input.GetAxis("Mouse Y")*speed_rot,0,0);
    }
    void rot()
    {
        rotationX += Input.GetAxis("Mouse X") * 5f;
        rotationY -= Input.GetAxis("Mouse Y") * 5f;
        transform.eulerAngles = new Vector3(rotationY, rotationX, 0f);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "stena")
        {
            Debug.Log("pp");
        }
    }
}
