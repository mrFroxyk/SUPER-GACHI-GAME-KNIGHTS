using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSXExample_CameraMovement : MonoBehaviour
{
    public float rotationX = 0f;
    public float rotationY = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void rot()
    {
        rotationX += Input.GetAxis("Mouse X")*5f;
        rotationY -= Input.GetAxis("Mouse Y")*5f;
        transform.eulerAngles = new Vector3(rotationY, rotationX, 0f);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "stena")
        {
            Debug.Log("pp");
        }
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
        transform.localPosition += new Vector3(transform.right.x, 0, transform.right.z) * Input.GetAxis("Horizontal") * speed*2.5f * Time.deltaTime;
    }
}
