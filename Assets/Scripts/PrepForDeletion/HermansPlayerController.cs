using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HermansPlayerController : MonoBehaviour
{

//    public float speed = 18;
//
//    public float turnSpeed = 60;
//
//    private Rigidbody rig;

	public float speed = 10.0F; 
	public float rotationSpeed = 100.0F; 

	public bool isOnground;
	public Transform groundCheck;
	public LayerMask whatIsTheGround;
	float groundSphere = 0.3f;

	public float jumpForce;
	public float jumpForceHold;
	public float maxJumpTime;
	float maxJumpTimeInternal;


    // Use this for initialization
    void Start()
    {
//        rig = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

		Move ();
		Jump ();

//        float hAxis = Input.GetAxis("Horizontal");
//        float vAxis = Input.GetAxis("Vertical");
//
//        float rStickX = Input.GetAxis("X360_RStickX");
//
//        Vector3 movement = transform.TransformDirection(new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime);
//
//        rig.MovePosition(transform.position + movement);
//
//        Quaternion rotation = Quaternion.Euler(new Vector3(0, rStickX, 0) * turnSpeed * Time.deltaTime);
//
//        transform.Rotate(new Vector3(0, rStickX, 0) * turnSpeed * Time.deltaTime);

    }

	void Move()
	{
		float translation = Input.GetAxis ("Vertical") * speed; 
		float rotation = Input.GetAxis ("Horizontal") * rotationSpeed; 
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime; 
		transform.Translate (0, 0, translation); 
		transform.Rotate (0, rotation, 0);

	}

	void Jump()
	{
		if (isOnground && Input.GetButtonDown("Jump"))
		{
			GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));
			maxJumpTimeInternal = maxJumpTime;
		}

		if(Input.GetButton("Jump") && GetComponent<Rigidbody>().velocity.y > 1 && maxJumpTimeInternal > 0)
		{
			GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForceHold, 0));
			maxJumpTimeInternal = maxJumpTimeInternal -1;
		}

	
	}

	void FixedUpdate()
	{
		isOnground = Physics.OverlapSphere(groundCheck.position, groundSphere, whatIsTheGround).Length > 0;
	}
}
