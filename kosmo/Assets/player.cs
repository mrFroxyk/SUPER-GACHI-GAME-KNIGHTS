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

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CharacterController = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        
        move(ref PredPolet, ref speed);
        Debug.Log(PredPolet);

        
    }
    void move ( ref Vector3 PredPolet, ref float speed)
    {
        Vector3 vector = Vector3.zero;
        Vector3 vector1 = new Vector3(Input.GetAxis("Horizontal") * 2f, 0, Input.GetAxis("Vertical"));
        float horX = Input.GetAxis("Horizontal") * 2f;
        float VertZ = Input.GetAxis("Vertical");
        if (CharacterController.isGrounded)
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
            PredPolet = new Vector3(Input.GetAxis("Horizontal") * 2f, 0, Input.GetAxis("Vertical"));
            vector = PredPolet;
            //vector = new Vector3(Input.GetAxis("Horizontal") * 2f, 0, Input.GetAxis("Vertical"));
            Jspeed = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                Jspeed = JumpSpeed;
                
            }
        }
        if (CharacterController.isGrounded == false)
        {
            vector = new Vector3(PredPolet.x+ horX/2, PredPolet.y, PredPolet.z+VertZ/2);
        }
        vector *= speed;
        vector = cam.transform.TransformDirection(vector);
        Jspeed -= graviti * Time.deltaTime;
        vector.y = Jspeed;
        vector.y -= graviti * Time.deltaTime;
        CharacterController.Move(vector * Time.deltaTime);


    }
}