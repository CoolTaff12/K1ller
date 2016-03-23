using UnityEngine;
using System.Collections;

public class hermControls : MonoBehaviour 
{
	public Animator anim;
	public float speed = 10.0F; 
	public float rotationSpeed = 100.0F; 
	public testThrowBall testTB;
	public Rigidbody character;
	public GameObject character2;

	public bool isOnground;
	public Transform groundCheck;
	public LayerMask whatIsTheGround;
	float groundSphere = 0.3f;

	public float jumpForce;
	public float jumpForceHold;
	public float maxJumpTime;
	float maxJumpTimeInternal;

	void Start ()
	{
		anim = GetComponent<Animator>();
		testTB = GetComponent<testThrowBall>();
//		anim.SetBool("isIdle", true);

	}

	void Update () 
	{ 
		if (isOnground && Input.GetButtonDown("Jump"))
		{
			GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));
			maxJumpTimeInternal = maxJumpTime;
			anim.SetTrigger("isJumping");
		}

		if(Input.GetButton("Jump") && GetComponent<Rigidbody>().velocity.y > 1 && maxJumpTimeInternal > 0)
		{
			GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForceHold, 0));
			maxJumpTimeInternal = maxJumpTimeInternal -1;
		}
		float translation = Input.GetAxis("Vertical") * speed; 
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed; 
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime; 
		transform.Translate(0, 0, translation); 
		transform.Rotate(0, rotation, 0);


		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			anim.SetTrigger("isThrowing");
			testTB.ThrowBall();


		}


		if (translation != 0)
		{
			anim.SetBool("isJogging", true);
			anim.SetBool("isIdle", false);
		}
		else
		{
			anim.SetBool("isJogging", false);
			anim.SetBool("isIdle", true);
		}



	}

	void FixedUpdate()
	{
		isOnground = Physics.OverlapSphere(groundCheck.position, groundSphere, whatIsTheGround).Length > 0;
	}
}
