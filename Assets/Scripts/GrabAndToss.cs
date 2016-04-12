using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class GrabAndToss : NetworkBehaviour
{

	RaycastHit hit;
	public float rayDistance = 4f; //Length of Ray. Default set to 5.
	public float rayRadius = 0.75f; //Radius of Ray. Default set to 0.75
	public float tossForce = 20f; //Force added to ball when tossed. Default set to 20;
//	public int killed = 1; //Is this character killed? 1 for true, 0 for false;
	[SyncVar]
	public bool holdingBall; //Is this character holding a ball?
	public bool throwing = false;
	public DodgeBallBehaviour ballScript; //Script on the ball hit by the players' raycast.
	public AssignPlayerInfo assignInfo; //Script to set initial info such as teamNumber.
	public Animator anim;//Animator attached to the player.
	public GameObject fpc; //FirstPersonController connected to the player.
	public GameObject head; //Head of the player;
	public GameObject currentBall; //Ball that is currently being held.
	public GameObject holdPos; //Position of the held ball.
	public GameObject infoHandler; //PlayerInfoHandler found in scene.

	public PlayerInfo playerInfo;
	public NetworkCharacterInfo charInfo;



	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
		playerInfo = gameObject.GetComponent<PlayerInfo> ();
    }



    // Update is called once per frame
    void Update ()
	{



//DEBUG//
Debug.DrawRay (head.transform.position, head.transform.forward, Color.green, rayDistance);
		if (Physics.SphereCast (head.transform.position, rayRadius, head.transform.forward, out hit, rayDistance)) {
			if (!isLocalPlayer) {
				return;
			}
			if (hit.collider.GetComponent<DodgeBallBehaviour> () != null) {
				print ("Ball!");
				if (Input.GetKey (KeyCode.E) || CrossPlatformInputManager.GetButton ("Fire1") && !holdingBall && !playerInfo.dead) {
					if (!hit.collider.GetComponent<DodgeBallBehaviour> ().pickedUp) {
					currentBall = hit.collider.gameObject;
					Cmd_GetPickedUp (currentBall , gameObject);
					holdingBall = true;
					}

				}
			}
		}
//--THROW BALL--//
		if (CrossPlatformInputManager.GetButton ("Fire2")) {
			if (!isLocalPlayer) {
				return;
			}
			if (!holdingBall) {
				anim.SetBool ("isThrowing", false);
				return;
			}
//			Rigidbody brb = currentBall.GetComponent<Rigidbody> ();
//			currentBall.transform.parent = null;
			if (!throwing && holdingBall) {
				throwing = true;
				anim.SetBool ("isThrowing", true);
			} 
			holdingBall = false;
			StartCoroutine(StartThrow(0.5F));
			Cmd_Shoot (currentBall);
//			brb.AddForce(head.transform.forward * tossForce);
			currentBall = null;
			ballScript = null;

		}
		if (Input.GetButtonUp ("Fire2") && throwing) {
			throwing = false;
			anim.SetBool ("isThrowing", false);
		}

	}
		
	[Command]
	public void Cmd_Shoot(GameObject bs){
		ballScript = bs.GetComponent<DodgeBallBehaviour> ();
		ballScript.Rpc_Shoot ();
	}
	[Command]
	void Cmd_GetPickedUp(GameObject bs, GameObject go){
		ballScript = bs.GetComponent<DodgeBallBehaviour> ();
		ballScript.Rpc_GetPickedUp (go);
	}


	IEnumerator StartThrow(float waitTime) 
	{
		yield return new WaitForSeconds(waitTime);
	}
}
