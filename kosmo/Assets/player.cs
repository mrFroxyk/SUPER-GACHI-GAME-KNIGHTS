using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    CharacterController CharacterController;
    public float speed = 2f;
    public GameObject cam;
    public GameObject PoletCam;
    public GameObject[] from;

    public float JumpSpeed = 3f;
    public float graviti1 = 4f;
    private float Jspeed = 0f;
    private Vector3 PredPolet = new Vector3(0,0,0);
    public float maxDist = 0.2f;
    private float tim = 0f;

    public  Animation animation;
    public AnimationClip a;
    public Animator anim;

    private float TransZ;
    private float TransX;
    public Text text;
    private float MaxSpeeddd;
    private int frame;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        //animation = cam.GetComponent<Animation>();
        anim = cam.GetComponent<Animator>();
        anim.SetBool("IsWalk", false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

         float TransZ = transform.position.z;
         float TransX = transform.position.x;
         MaxSpeeddd = 0f; 
    }
    
    void Update()
    {
        move(ref PredPolet, ref speed, ref tim, ref graviti1);
        sc();   //если хотетите убрать текст со скоростью, закоментете это
        //Debug.Log($"{NaZemle()} {graviti}"); //дебагер)))
    }
    
    void move ( ref Vector3 PredPolet, ref float speed,ref float tim,ref float graviti1)
    {
        float graviti = graviti1;

        Vector3 vector = Vector3.zero;
        float horX = Input.GetAxis("Horizontal");
        float VertY = Input.GetAxis("Vertical");
        if (NaZemle())
        {
            graviti = graviti1*20;
            if (Mathf.Abs(horX)>0.2 ^ Mathf.Abs(VertY) > 0.2)
            {
                //animation.Play(a.name);
                anim.SetBool("IsWalk", true);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                     
                }
                else if (Input.GetKey(KeyCode.LeftControl))
                {

                }
                else
                {
                }
            }
            else
            {
                anim.SetBool("IsWalk", false);
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
            PredPolet = new Vector3(horX, 0, VertY); 
            vector = PredPolet;
            Jspeed = 0f;
            if (Input.GetButton("Jump"))
            {
                graviti = graviti1;
            }
            if (Input.GetButtonDown("Jump"))
            {
                Jspeed = JumpSpeed;
            }
        }
        else
        {
            graviti = graviti1;
            speed = 3f;
            vector = new Vector3(PredPolet.x+ horX/2, PredPolet.y, PredPolet.z);; //значительно фиксирует вектор полета
            anim.SetBool("IsWalk", false);
        }
        Debug.Log($"{NaZemle()} {graviti}  {transform.position.y}"); //дебагер)))

        vector *= speed;
        vector = PoletCam.transform.TransformDirection(vector);
        Jspeed -= graviti * Time.deltaTime;
        vector.y = Jspeed;
        vector.y -= graviti * Time.deltaTime; //гравитация 
        CharacterController.Move(vector * Time.deltaTime);

        
    }

    public bool NaZemle()
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
    float SchetchikSpeed(ref float TransZ, ref float TransX)
    {
        float TransZ2 = transform.position.z;
        float TransX2 = transform.position.x;
        float deltaTransZ = TransZ2 - TransZ;
        float deltaTransX = TransX2 - TransX;

        TransZ = transform.position.z;
        TransX = transform.position.x;

        float rasstoianie = Mathf.Sqrt((deltaTransZ * deltaTransZ + deltaTransX * deltaTransX));
        float speed = rasstoianie / Time.deltaTime;

        return speed;
    }
    void sc()
    {
        frame++;
        if (frame == 3) { MaxSpeeddd = 0f; }
        float z = (SchetchikSpeed(ref TransZ, ref TransX));
        if (z > MaxSpeeddd)
        {
            MaxSpeeddd = z;
        }
        text.text = $"SPEED: { z} " + "\n" + $"MaxSpeed: {MaxSpeeddd}";
    }
}
//IEnumerator KachanieGolovoi()
//{
//    Debug.Log("222");
//    for (float i=0f; i < 0.2; i += Time.deltaTime)
//    {
//        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 1 + (Mathf.Sin(i * 20)*1), transform.position.z);
//        yield return null;
//    }
//}