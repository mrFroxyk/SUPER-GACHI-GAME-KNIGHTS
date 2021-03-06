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
    public GameObject CamY;
    public GameObject PoletCam;
    public GameObject[] from;
    public float JumpSpeed = 3f;
    public float graviti1 = 4f;
    private float Jspeed = 0f;
    private Vector3 PredPolet = new Vector3(0,0,0);
    public float maxDist = 0.2f;
    private float TimeInSky = 0f;
    private float HorDO;
    private float VertDO;

    public Animator anim;

    public GameObject kaiser;
    public new AudioClip audio;
    public AudioClip KAISER;
    public AudioClip run;
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
        AudioComponent1 = kaiser.GetComponent<AudioSource>();
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
        move(ref PredPolet,ref HorDO, ref VertDO, ref speed, ref graviti1);
        sc();   //если хотетите убрать текст со скоростью, закоментете это
        SoundManager();
    }

    void move(ref Vector3 PredPolet, ref float HorDO, ref float VertDO, ref float speed, ref float graviti1)
    {
        float graviti = graviti1;

        Vector3 vector = Vector3.zero;
        float horX = Input.GetAxis("Horizontal");
        float VertY = Input.GetAxis("Vertical");

        if (Mathf.Sqrt(horX * horX + VertY * VertY) > 1)  //не дает ускорится по диагонали
        {
            float tangens = Mathf.Atan((VertY) / (horX));
            if (VertY > 0) { VertY = Mathf.Abs(Mathf.Sin(tangens)); }
            else { VertY = -Mathf.Abs(Mathf.Sin(tangens)); }
            if (horX > 0) { horX = Mathf.Cos(tangens); }
            else { horX = -Mathf.Cos(tangens); }
        }

        if (NaZemle())
        {
            TimeInSky = 0f;
            Vector3 vector3 = CamY.transform.TransformDirection(new Vector3(horX, 0, VertY));
            vector = vector3;
            VertDO = vector3.z;
            HorDO = vector3.x;

            graviti = graviti1 * 20;
            Ray ray = new Ray(from[0].transform.position, -transform.up);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "lestnica" )
                {
                    graviti = graviti1 * 20;
                }
            }
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

            if (Input.GetKey(KeyCode.LeftControl)) { speed = 2f; } //задаем скорость
            else if (Input.GetKey(KeyCode.LeftShift)) { speed = 5f; }
            else { speed = 3f; }
            
            if (VertY < -0.2) //если бежим спиной
            {
                speed = Mathf.Clamp(speed, 0f, 2.5f);
            }
            Jspeed = 0f;
            if (Input.GetButton("Jump")) { graviti = graviti1; Jspeed = JumpSpeed; }
        }

        else //если не замле
        {
            graviti = graviti1;;
            if (speed == 2f) { speed = 1.5f; }
            else if (speed == 3f) { speed = 2.5f; }
            else if (speed == 5f) { speed = 4f; }
            //Ray ray = new Ray(from[0].transform.position, -transform.up);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit))
            //{
            //    if (hit.collider.tag == "lestnica" && hit.distance <0.2 && Input.GetKeyDown(KeyCode.Space)==false)
            //    {
            //        graviti = graviti1 * 20;
            //    }
            //}
            AudioComponent.Stop();
            zvyk = true;
            
            TimeInSky += Time.deltaTime;

            Vector3 vector3 = CamY.transform.TransformDirection(new Vector3(horX / 100, 0, VertY  / 100));
            HorDO += vector3.x;
            VertDO += vector3.z;
            HorDO = Mathf.Clamp(HorDO, -1f, 1f);
            VertDO = Mathf.Clamp(VertDO, -1f, 1f);
            if (Mathf.Sqrt(HorDO * HorDO + VertDO * VertDO) > 1)  //не дает ускорится по диагонали
            {
                float tangens = Mathf.Atan((VertDO) / (HorDO));

                if (VertDO > 0) { VertDO = Mathf.Abs(Mathf.Sin(tangens)); }
                else { VertDO = -Mathf.Abs(Mathf.Sin(tangens)); }
                if (HorDO > 0) { HorDO = Mathf.Cos(tangens); }
                else { HorDO = -Mathf.Cos(tangens); }
            }
            
            vector = new Vector3(HorDO, 0, VertDO); 

            anim.SetBool("IsWalk", false);
        }
        //Debug.Log($"{NaZemle()} {graviti}  {transform.position.y}"); //дебагер)))
        vector *= speed;
   
        //vector = PoletCam.transform.TransformDirection(vector);
        Jspeed -= graviti * Time.deltaTime;
        vector.y = Jspeed;
        vector.y -= graviti * Time.deltaTime; //гравитация 
        CharacterController.Move(vector * Time.deltaTime);   
    }
    float SpeedPoletManager(float TimeInSky)   //вначале мы можем значительно менять скорость, потому я так хочу
    {
        if (TimeInSky < 0.5f)
        {
            return 0.6f;
        }
        else if (TimeInSky < 3)
        {
            return 0.5f;
        }
        else
        {
            return 0.3f;
        }
    }
    void SoundManager()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioComponent1.Stop();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioComponent1.PlayOneShot(KAISER);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioComponent1.pitch = 0.6f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioComponent1.pitch = 0.7f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AudioComponent1.pitch = 0.8f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AudioComponent1.pitch = 0.9f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AudioComponent1.pitch = 1f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            StartCoroutine(pitc());
            
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
    IEnumerator pitc()
    {
        for (float i = 0f; i < 8f; i += Time.deltaTime)
        {
            Debug.Log("ff");
            AudioComponent1.pitch = 1f-i/20; 
            yield return null;
        }
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