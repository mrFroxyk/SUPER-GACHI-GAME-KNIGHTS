using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    CharacterController CharacterController;
    public float speed = 2f;
    public Transform cam;
    private float rotationX = 0f;
    public float JumpSpeed = 100f;
    public float graviti =9.8f;
    private float Jspeed = 0f;
    public float rotationY = 0f;


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
            speed = 5f;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = 1f;
        }
        else
        {
            speed = 3f;
        }
        Quaternion qua = new Quaternion(0, cam.rotation.y, 0, 1);
        transform.rotation = qua;
        move(speed);
    }
    void move(float speed)
    {
        rotationX += Input.GetAxis("Mouse X") * 3f;
        transform.eulerAngles = new Vector3(0, rotationX, 0f);
        Vector3 vector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") / 2f);
        Vector3 PredPolet = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") / 2f);
        if (CharacterController.isGrounded)
        {
            vector = new Vector3(Input.GetAxis("Horizontal") * 2f, 0, Input.GetAxis("Vertical"));
            Jspeed = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                Jspeed = JumpSpeed;
            
            }
        }
        else
        {

        }
        vector *= speed;
        vector = transform.TransformDirection(vector);
        
        Jspeed -= graviti * Time.deltaTime;
        vector.y = Jspeed;

        vector.y -= graviti * Time.deltaTime;
        CharacterController.Move(vector * Time.deltaTime);
        Debug.Log( CharacterController.isGrounded);

        
    }
}