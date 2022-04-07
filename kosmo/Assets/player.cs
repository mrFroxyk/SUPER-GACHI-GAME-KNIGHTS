using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    CharacterController CharacterController;
    public float speed = 2f;
    public GameObject cam;
    
    public float JumpSpeed = 100f;
    public float graviti =9.8f;
    private float Jspeed = 0f;
    public Vector3 PredPolet = new Vector3(0,0,0);
    public Vector3 vector = new Vector3(0, 0, 0);

    private float rotationY = 0f;
    private float rotationX = 0f;
    private float CamY = 0f;




    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CharacterController = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {

        Rot();
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
        //rotationX += Input.GetAxis("Mouse X") * 3f * Time.deltaTime * 50;
        ////CamY = cam.eulerAngles.y;
        //Vector3 qua = new Vector3(0, rotationX, 0);

        
        move(speed);
        
    }
    void Rot()
    {
        rotationX += Input.GetAxis("Mouse X") * 3f * Time.deltaTime * 50;
        rotationY -= Input.GetAxis("Mouse Y") * 3f * Time.deltaTime * 50;
        //transform.eulerAngles = new Vector3(0, , 0f);
        rotationY = Mathf.Clamp(rotationY, -90f, 75f);
        transform.eulerAngles = new Vector3(rotationY, rotationX, 0f);
        Debug.Log(transform.eulerAngles.x);
    }
    void move(float speed)
    {


        //transform.eulerAngles = new Vector3(0, rotationX, 0f);
        Vector3 vector = new Vector3(Input.GetAxis("Horizontal") * 2f, 0, Input.GetAxis("Vertical"));
        
        if (CharacterController.isGrounded)
        {
            //Vector3 PredPolet = new Vector3(Input.GetAxis("Horizontal") * 2f, 0, Input.GetAxis("Vertical"));
            //vector = new Vector3(Input.GetAxis("Horizontal") * 2f, 0, Input.GetAxis("Vertical"));
            Jspeed = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                Jspeed = JumpSpeed;
            
            }
        }
        else
        {
            //vector = PredPolet;
        }
        

        vector *= speed;
        vector = transform.TransformDirection(vector);

        Jspeed -= graviti * Time.deltaTime;
        vector.y = Jspeed;

        vector.y -= graviti * Time.deltaTime;
        CharacterController.Move(vector * Time.deltaTime);
        //Debug.Log( CharacterController.isGrounded);

        
    }
}