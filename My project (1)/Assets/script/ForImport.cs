using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ForImport : MonoBehaviour
{

	public CharacterController2D2 controller;
	private Animator anim;

	public float runSpeed = 40f;

	float horizontalMove;
	bool jump = false;
	//public check_stena check_Stena;
	private void Start()
	{
		anim = GetComponent<Animator>();
		//check_Stena = GetComponentInChildren<check_stena>();
		//check_Stena2 = GetComponentInChildren<check_stena>();
	}
	private States State
	{
		get { return (States)anim.GetInteger("state"); }
		set { anim.SetInteger("state", (int)value); }
	}
	public enum States
	{
		Idle,
		Run,
		Jump

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
