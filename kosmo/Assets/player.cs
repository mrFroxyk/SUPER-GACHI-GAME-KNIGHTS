using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    CharacterController CharacterController;
    public float speed = 2f;
    public Transform cam;
    private float rotationX = 0f;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CharacterController = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 4f;
        }
        else
        {
            speed = 2f;
        }
        Quaternion qua = new Quaternion(0, cam.rotation.y, 0, cam.rotation.w);
        transform.rotation = qua;
        move();
    }
    void move()
    {
        rotationX += Input.GetAxis("Mouse X") * 3f;
        transform.eulerAngles = new Vector3(0, rotationX, 0f);
        Vector3 vector = new Vector3(Input.GetAxis("Horizontal")*2f, 0, Input.GetAxis("Vertical"));
        vector = transform.TransformDirection(vector);
        vector *= speed;
        //CharacterController.SimpleMove(vector);
        if (Input.GetButtonDown("Jump"))
        {
        }
            
        vector.y -= 200.0F * Time.deltaTime;
        CharacterController.Move(vector * Time.deltaTime);

        
    }
}