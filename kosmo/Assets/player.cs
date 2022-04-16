using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class player : MonoBehaviour
{
    CharacterController CharacterController;  //переменные контроя движения
    public float speed = 2f;
    public GameObject cam;
    public GameObject PoletCam;
    public GameObject[] from;
    public float JumpSpeed = 3f;
    public float graviti1 = 4f;
    private float Jspeed = 0f;
    private Vector3 PredPolet = new Vector3(0,0,0);
    public float maxDist = 0.2f;
    private float TimeInSky = 0f;
    private float HorX2 = 0f;
    private float VertY2 = 0f;
    private float xx;
    private float yy;

    public new Animation animation;
    public AnimationClip a;
    public Animator anim;

    public new AudioClip audio;
    public AudioClip KAISER;
    private AudioSource AudioComponent;
    private AudioSource AudioComponent1;
    private bool zvyk = true;

    private float TransZ; //считаем скорость
    private float TransX;
    public Text text;
    private float MaxSpeeddd;
    private int frame;


    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        AudioComponent = from[0].GetComponent<AudioSource>();
        AudioComponent1 = from[1].GetComponent<AudioSource>();
        //animation = cam.GetComponent<Animation>();
        anim = cam.GetComponent<Animator>();
        anim.SetBool("IsWalk", false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

         float TransZ = transform.position.z;
         float TransX = transform.position.x;
         MaxSpeeddd = 0f;
        //Time.timeScale = 0.2f;
    }
    
    void Update()
    {
        
        move(ref PredPolet, ref speed, ref graviti1);
        sc();   //если хотетите убрать текст со скоростью, закоментете это
        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioComponent1.PlayOneShot(KAISER);
        }
    }
    void move(ref Vector3 PredPolet, ref float speed, ref float graviti1)
    {
        float graviti = graviti1;

        Vector3 vector = Vector3.zero;
        float horX = Input.GetAxis("Horizontal");
        float VertY = Input.GetAxis("Vertical");

        if (Mathf.Sqrt(horX * horX + VertY * VertY) > 1)  //не дает ускорится по диагонали
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
            TimeInSky = 0f;
            PredPolet = new Vector3(horX, 0, VertY);
            vector = PredPolet;

            HorX2 = horX;
            VertY2 = VertY;

            graviti = graviti1 * 20;

            if (Mathf.Abs(VertY) > 0.2) { //если двигаемся 
                anim.SetBool("IsWalk", true); 
                if (zvyk)
                {
                    zvyk = false;
                    if (AudioComponent.isPlaying == false)
                    {
                        AudioComponent.PlayOneShot(audio);
                        Invoke("Pere", 3f);
                    }
                    else
                    {
                        zvyk = true;
                    }
                }
            }
            else 
            { 
                anim.SetBool("IsWalk", false);
                AudioComponent.Stop();
                zvyk = true;
            }


            if (Input.GetKey(KeyCode.LeftControl))
            {
                anim.SetBool("prisel", true);
            }
            else
            {
                anim.SetBool("prisel", false);
            }

            if (Input.GetKey(KeyCode.LeftShift)) { speed = 5.5f; } //задаем скорость
            else if (Input.GetKey(KeyCode.LeftControl)) { speed = 2f; }
            else { speed = 3.5f; }
            
            if (PredPolet.z < -0.2) //если бежим спиной
            {
                speed = Mathf.Clamp(speed, 0f, 2.5f);
                //speed /= (Mathf.Abs(Mathf.Clamp(PredPolet.z, 0.5f, 1f)));
            }
            Jspeed = 0f;
            if (Input.GetButton("Jump")) { graviti = graviti1; }
            if (Input.GetButtonDown("Jump")) { Jspeed = JumpSpeed; graviti = graviti1; }
        }

        else //если не замле
        {
            AudioComponent.Stop();
            zvyk = true;

            graviti = graviti1;
            speed = 3f;
            TimeInSky += Time.deltaTime;

            if (HorX2 > 0)
            {
                float z = PredPolet.x + horX * SpeedPoletManager(TimeInSky) * Time.deltaTime;
                //float delt = Mathf.Min(Mathf.Abs(z - 0.5f), Mathf.Abs(z - HorX2));
                xx = Mathf.Clamp(z, -1f , HorX2);
            }
            else
            {
                float z = PredPolet.x + horX * SpeedPoletManager(TimeInSky) * Time.deltaTime;
                //float delt = Mathf.Min(Mathf.Abs(z - 1f), Mathf.Abs(z - HorX2));
                xx = Mathf.Clamp(z, HorX2, 1f );
            }
            if (VertY2 > 0)
            {
                float z = PredPolet.z + VertY * SpeedPoletManager(TimeInSky) * Time.deltaTime;
                //float delt = Mathf.Min(Mathf.Abs(z - 0.25f), Mathf.Abs(z - VertY2));
                yy = Mathf.Clamp(z, -1f, VertY2);
            }
            else
            {
                float z = PredPolet.z + VertY * SpeedPoletManager(TimeInSky) * Time.deltaTime;
                //float delt = Mathf.Min(Mathf.Abs(z - 0.25f), Mathf.Abs(z - VertY2));
                yy = Mathf.Clamp(z, VertY2,1f);
            }
            //Debug.Log($"PredPolet {PredPolet}       HorX2 {HorX2}       VertY2 {VertY2}");
            
            PredPolet =new Vector3(xx, 0, yy);
            vector = PredPolet;
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
    float SpeedPoletManager(float TimeInSky)   //вначале мы можем значительно менять скорость, потому я так хочу
    {
        if (TimeInSky < 0.5f)
        {
            return 3.5f;
        }
        else if (TimeInSky < 1)
        {
            return 1.5f;
        }
        else
        {
            return 0.5f;
        }
    }
    void Pere()  //перезарядка звука
    {
        zvyk = true;
    }
    public bool NaZemle()  //мы на земле?
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

        if (CharacterController.isGrounded == false | Dist>maxDist) { return false; }

        else { return CharacterController.isGrounded; }
    }
    float SchetchikSpeed(ref float TransZ, ref float TransX) //считаем скорость
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
    void sc() //запомниаем максимальную скорость
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
    //IEnumerator KachanieGolovoi()
    //{
    //    for (float i = 0f; i < 0.2; i += Time.deltaTime)
    //    {
    //        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 1 + (Mathf.Sin(i * 20) * 1), transform.position.z);
    //        yield return null;
    //    }
    //}
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