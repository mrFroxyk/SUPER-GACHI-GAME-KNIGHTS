using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueMove : MonoBehaviour
{

	public CharacterController2D2 controller;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	bool MoveVStene = false;
	public check_stena check_Stena;
    private void Start()
    {
		//check_Stena = GetComponentInChildren<check_stena>();
		//check_Stena2 = GetComponentInChildren<check_stena>();
	}
    void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
		//if (check_Stena.Check())
  //      {
		//	Debug.Log(check_Stena.Check());
  //      }
  //      else
  //      {
		//	Debug.Log(check_Stena.Check());
		//}
	
	}


	void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		jump = false;
	}
}


//if (Input.GetButtonDown("Crouch"))
//{
//    crouch = true;
//}
//else if (Input.GetButtonUp("Crouch"))
//{
//    crouch = false;
//}