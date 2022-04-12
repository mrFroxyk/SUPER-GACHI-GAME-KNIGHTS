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
    private Vector3 PredPoletREAL = new Vector3(0, 0, 0);
    public float maxDist = 0.2f;
    private float tim = 0f;
    private float TimeOnGround = 0f;
    private float HorX2 = 0f;
    private float VertY2 = 0f;

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
        move(ref PredPolet, ref PredPoletREAL, ref HorX2,ref VertY2, ref speed, ref tim, ref graviti1);
        sc();   //если хотетите убрать текст со скоростью, закоментете это
        //Debug.Log($"{NaZemle()} {graviti}"); //дебагер)))
    }
    
    void move ( ref Vector3 PredPolet, ref Vector3 PredPoletREAL,ref float HorX2,ref float VertY2, ref float speed,ref float tim,ref float graviti1)
    {
        float graviti = graviti1;

        Vector3 vector = Vector3.zero;
        float horX = Input.GetAxis("Horizontal");
        float VertY = Input.GetAxis("Vertical");

        if ( Mathf.Sqrt (horX* horX+ VertY* VertY) > 1)
        {
            float tangens = Mathf.Atan((VertY) / (horX));
            //Debug.Log($"{tangens}  {horX} {VertY}");
            if (VertY > 0) { VertY = Mathf.Abs(Mathf.Sin(tangens)); }
            else { VertY = -Mathf.Abs(Mathf.Sin(tangens)); }
            if (horX > 0) { horX = Mathf.Cos(tangens); }
            else { horX = -Mathf.Cos(tangens); }
        }
        if (NaZemle())
        {
            TimeOnGround = 0f;
            graviti = graviti1*20;
            if (Mathf.Abs(VertY) > 0.2)
            {
                //animation.Play(a.name);
                anim.SetBool("IsWalk", true);
            }
            else
            {
                anim.SetBool("IsWalk", false);
            }
            
            if (Input.GetKey(KeyCode.LeftShift)) { speed = 5f; }
            else if (Input.GetKey(KeyCode.LeftControl)){ speed = 1f;}
            else { speed = 3f; }

            PredPolet = new Vector3(horX, 0, VertY);
            PredPoletREAL = PredPolet; //WTF?
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

            TimeOnGround += Time.deltaTime;
            //HorX2 = PredPolet.x;
            //VertY2 = PredPolet.z;
            //Debug.Log($"{HorX2 - PredPoletREAL.x} {VertY2 - PredPoletREAL.z}");
            float DeltaHorX2 = HorX2 - PredPoletREAL.x;
            float DeltaVertY2 = VertY2 - PredPoletREAL.z;
            Debug.Log(PredPolet);
            //Debug.Log(PredPolet);
            if (TimeOnGround < 0.5)
            {
                PredPolet = PredPolet + new Vector3(horX / 30, 0, VertY / 30);
            }
            else if (0.5 < TimeOnGround && TimeOnGround < 1)
            {
                PredPolet = PredPolet + new Vector3(horX / 50, 0, VertY / 50);
            }
            else
            {
                PredPolet = PredPolet + new Vector3(horX / 80, 0, VertY / 80);
            }
            HorX2 = PredPolet.x;
            VertY2 = PredPolet.z;

            if (Mathf.Sqrt(HorX2 * HorX2 + VertY2 * VertY2) > 1)
            {
                float tangens = Mathf.Atan((VertY2) / (HorX2));
                if (VertY2 > 0) { VertY2 = Mathf.Abs(Mathf.Sin(tangens)); }
                else { VertY2 = -Mathf.Abs(Mathf.Sin(tangens)); }
                if (HorX2 > 0) { HorX2 = Mathf.Cos(tangens); }
                else { HorX2 = -Mathf.Cos(tangens); }
            }
            //PredPolet = new Vector3(HorX2, PredPolet.y, VertY2);

            //PredPolet = new Vector3(PredPolet.x + horX / 20, PredPolet.y, PredPolet.z + VertY / 20);
            vector = PredPolet; //значительно фиксирует вектор полета
            anim.SetBool("IsWalk", false);
        }
        //Debug.Log($"{NaZemle()} {graviti}  {transform.position.y}"); //дебагер)))



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