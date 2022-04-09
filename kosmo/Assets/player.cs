using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    CharacterController CharacterController;
    public float speed = 2f;
    public GameObject cam;
    public GameObject[] from;

    public float JumpSpeed = 100f;
    public float graviti =9.8f;
    private float Jspeed = 0f;
    public Vector3 PredPolet = new Vector3(0,0,0);
    public Vector3 vector = new Vector3(0, 0, 0);
    public float maxDist = 0.2f;
    public float tim = 0f;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CharacterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        move(ref PredPolet, ref speed, ref tim);
    }
    void move ( ref Vector3 PredPolet, ref float speed,ref float tim)
    {
        Vector3 vector = Vector3.zero;
        float horX = Input.GetAxis("Horizontal");
        float VertY = Input.GetAxis("Vertical");
        if (NaZemle())
        {
            if (Mathf.Abs(horX)>0.2 || Mathf.Abs(VertY) > 0.2)
            {
                tim += Time.deltaTime;
            }
            else
            {
                tim = 0;
            }
            
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
            PredPolet = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); 
            vector = PredPolet;
            Jspeed = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                Jspeed = JumpSpeed;
            }
        }
        if (NaZemle() == false)

        {
            tim = 0;
            vector = new Vector3(PredPolet.x+ horX/2, PredPolet.y, PredPolet.z);; //значительно фиксирует вектор полета
        }
        cam.transform.position = new Vector3(transform.position.x, transform.position.y+1+(Mathf.Sin(tim * 10))/Time.deltaTime/1300, transform.position.z);
        vector *= speed;
        vector = cam.transform.TransformDirection(vector);
        Jspeed -= graviti * Time.deltaTime;
        vector.y = Jspeed;
        vector.y -= graviti * Time.deltaTime; //гравитация 
        CharacterController.Move(vector * Time.deltaTime);
        Debug.Log(NaZemle());
    }
    bool NaZemle()
    {
        float Dist = 0f;
        float Ploshad = 0f;

        foreach (GameObject i in from)
        {
            Ray ray = new Ray(i.transform.position, -transform.up);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "stena" && hit.distance < 0.3) { return true; }
                if (hit.distance < 0.3) { Ploshad += 1; }
                Dist += hit.distance;
                Debug.DrawRay(i.transform.position, -transform.up, Color.red, 1f);
            }
        }
        Dist /= 11;

        if (CharacterController.isGrounded == false ^ Dist>maxDist) { return false; }

        else { return CharacterController.isGrounded; }

    }
}