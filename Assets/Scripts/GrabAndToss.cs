using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class GrabAndToss : NetworkBehaviour
{

	RaycastHit hit;
	[SerializeField]
	private float rayDistance = 4f; //Length of Ray. Default set to 5.
	[SerializeField]
	private float rayRadius = 0.75f; //Radius of Ray. Default set to 0.75
	[SerializeField]
	private float tossForce = 20f; //Force added to ball when tossed. Default set to 20;
	public float c_TossForce{get{return tossForce;}set{tossForce = value;}}
//	public int killed = 1; //Is this character killed? 1 for true, 0 for false;
	[SyncVar][SerializeField]
	private bool holdingBall; //Is this character holding a ball?
	[HideInInspector]
	public bool c_HoldingBall{get{return holdingBall;}}
	private bool throwing = false;
	private DodgeBallBehaviour ballScript; //Script on the ball hit by the players' raycast.
	private AssignPlayerInfo assignInfo; //Script to set initial info such as teamNumber.
	private Animator anim;//Animator attached to the player.
	[SerializeField]
	private GameObject fpc = null; //FirstPersonController connected to the player.
	public GameObject c_FPC {get{return fpc;}}
	[SerializeField]
	private GameObject head  = null; //Head of the player;
	public GameObject c_Head {get{return head;}}
	private GameObject currentBall; //Ball that is currently being held.
	public GameObject c_CurrentBall{get{return currentBall;}}
	[SerializeField]
	private GameObject holdPos  = null; //Position of the held ball.
	public GameObject c_HoldPos{get{return holdPos;}}
//	[SerializeField]
//	private GameObject fakeBall;
	private PlayerInfo playerInfo;
	private NetworkCharacterInfo charInfo;



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
//Debug.DrawRay (head.transform.position, head.transform.forward, Color.green, rayDistance);

		//Is the player looking at a ball?
		if (Physics.SphereCast (c_Head.transform.position, rayRadius, c_Head.transform.forward, out hit, rayDistance)) {
			if (!isLocalPlayer) {
				return;
			}
			if (hit.collider.GetComponent<DodgeBallBehaviour> () != null) {
//				print ("Ball!");
				//Pick up the ball
				if (CrossPlatformInputManager.GetButton ("Fire1") && !holdingBall && !playerInfo.c_Dead) {
					if (!hit.collider.GetComponent<DodgeBallBehaviour> ().b_PickedUp) {
					currentBall = hit.collider.gameObject;
					Cmd_GetPickedUp (currentBall , gameObject);
					holdingBall = true;
//					Cmd_toggleFake (fakeBall);

					}

				}
			}
		}
//--THROW BALL--//
		if (Input.GetButton ("Fire2")) {
			if (!isLocalPlayer) {
				return;
			}
			if (!holdingBall) {
				anim.SetBool ("isThrowing", false);
				return;
			}
			if (!throwing && holdingBall) {
				throwing = true;
				anim.SetBool ("isThrowing", true);
			} 
			holdingBall = false;
			StartCoroutine(StartThrow(0.5F));
			Cmd_Shoot (currentBall, tossForce);
//			brb.AddForce(head.transform.forward * tossForce);
			currentBall = null;
			ballScript = null;

		}
		if (Input.GetButtonUp ("Fire2") && throwing) {
			throwing = false;
			anim.SetBool ("isThrowing", false);
		}

	}
		/// <summary>
		/// Command that requests that the ball will shoot/get tossed.
		/// </summary>
		/// <param name="bs">Script attached to the ball</param>
		/// <param name="dir">What direction will the ball be tossed </param>
	[Command]
	public void Cmd_Shoot(GameObject bs, float force){
		ballScript = bs.GetComponent<DodgeBallBehaviour> ();
		ballScript.Rpc_Shoot (force);
	}
	/// <summary>
	/// Command that requests the ball to get picked up.
	/// </summary>
	/// <param name="bs">Script attached to the ball</param>
	/// <param name="go">The ball that is affected</param>
	[Command]
	void Cmd_GetPickedUp(GameObject bs, GameObject go){
		ballScript = bs.GetComponent<DodgeBallBehaviour> ();
		ballScript.Rpc_GetPickedUp (go);
	}
//	/// <summary>
//	/// Requests that the fake ball switches layer.
//	/// </summary>
//	/// <param name="go">fake ball object</param>
//	[Command]
//	void Cmd_toggleFake(GameObject go){
//		Rpc_toggleFake (go);
//	}
//	/// <summary>
//	/// Switches the layer of the fake ball
//	/// </summary>
//	/// <param name="go">fake ball object</param>
//	[ClientRpc]
//	void Rpc_toggleFake(GameObject go){
//		if (holdingBall) {
//			go.layer = 9;
//		}
//		if (!holdingBall) {
//			go.GetComponent<Renderer> ().material.mainTexture = currentBall.GetComponent<Renderer> ().material.mainTexture;
//			go.layer = 0;
//		}
//	}
	/// <summary>
	/// timer that lets the animation run a bit before proceeding.
	/// </summary>
	/// <returns></returns>
	/// <param name="waitTime">Time to wait before proceeding</param>
	IEnumerator StartThrow(float waitTime) 
	{
		yield return new WaitForSeconds(waitTime);
//		Cmd_toggleFake (fakeBall);
	}
}
