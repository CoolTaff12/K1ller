using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class GrabAndToss : NetworkBehaviour
{

	RaycastHit hit;
	public float rayDistance = 5f; //Length of Ray. Default set to 5.
	public float rayRadius = 0.75f; //Radius of Ray. Default set to 0.75
	public float tossForce = 20f; //Force added to ball when tossed. Default set to 20;
	[SyncVar]
	public int teamNumber; //Number of the team this character is on. Is set by AssignPlayerInfo Script.
	public int killed = 1; //Is this character killed? 1 for true, 0 for false;
	[SyncVar]
	public float health = 1f; //Amount of hits the character can take before dying.
	[SyncVar]
	public bool killable = true; //Can this character be killed?
	[SyncVar]
	public bool dead = false; //Is this character dead?
	[SyncVar]
	public bool holdingBall; //Is this character holding a ball?
	public Transform[] Bodyparts; //List of bodypart segments.
	public DodgeBallBehaviour ballInfo; //Script on the ball colliding with the player;
	public DodgeBallBehaviour ballScript; //Script on the ball hit by the players' raycast.
	public NetworkCharacterInfo assignInfo; //Script to set initial info such as teamNumber.
	public GameObject fpc; //FirstPersonController connected to the player;
	public GameObject head; //Head of the player;
	public GameObject currentBall; //Ball that is currently being held.
	public GameObject holdPos; //Position of the held ball.
	public GameObject networkMgr; //NetworkManager found in scene.

	// Use this for initialization
	void Start ()
	{
		Cmd_SetName (gameObject);
		Cmd_SetTeamNumber (gameObject); //Command sent to assign a team number to this player.

	}

	// Update is called once per frame
	void Update ()
	{
		if (health <= 0 && !dead) {
			//			RemoveChild ();
			Cmd_KillYourself(gameObject);
			dead = true;
			teamNumber = 0;
		}
//DEBUG//
//Debug.DrawRay (head.transform.position, head.transform.forward, Color.green, rayDistance);
		if (Physics.SphereCast (head.transform.position, rayRadius, head.transform.forward, out hit, rayDistance)) {
			if (!isLocalPlayer) {
				return;
			}
			if (hit.collider.GetComponent<DodgeBallBehaviour> () != null) {
				print ("Ball!");
				if (Input.GetKeyDown (KeyCode.E) || CrossPlatformInputManager.GetButtonDown ("Fire1") && !holdingBall) {
					if (!hit.collider.GetComponent<DodgeBallBehaviour> ().pickedUp) {
					currentBall = hit.collider.gameObject;
					Cmd_GetPickedUp (currentBall , gameObject);
					//ballScript.holdingPos = holdPos;
//					ballScript.pickedUp = true;
					holdingBall = true;
					}

				}
			}
		}
//--THROW BALL--//
		if (CrossPlatformInputManager.GetButtonDown ("Fire2") && holdingBall) {
			if (!isLocalPlayer) {
				return;
			}
			holdingBall = false;
//			Rigidbody brb = currentBall.GetComponent<Rigidbody> ();
//			currentBall.transform.parent = null;
			Cmd_Shoot (currentBall);
//			brb.AddForce(head.transform.forward * tossForce);
			currentBall = null;
			ballScript = null;

		}

	}
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ball")
		{
			ballInfo = col.gameObject.GetComponent<DodgeBallBehaviour>();
			if (teamNumber != ballInfo.thrownByTeam) {
				Cmd_TakeDamage ();
			}
		}
	}
	[Command]
	public void Cmd_TakeDamage() {
		health -= 1;

	}
	[Command]
	void Cmd_Shoot(GameObject bs){
		ballScript = bs.GetComponent<DodgeBallBehaviour> ();
		ballScript.Rpc_Shoot ();
	}
	[Command]
	void Cmd_GetPickedUp(GameObject bs, GameObject go){
		ballScript = bs.GetComponent<DodgeBallBehaviour> ();
		ballScript.Rpc_GetPickedUp (go);
	}
	[Command]
	public void Cmd_SetTeamNumber(GameObject go){
		networkMgr = GameObject.Find ("PlayerInfoHandler");
		Debug.Log ("PlayerSPawnHejHej");
		assignInfo = GetComponent<NetworkCharacterInfo> ();
		Debug.Log (go + " = GameObject We Are Looking For");
		assignInfo.Rpc_SetTeamNumber (go);
	}
	[Command]
	public void Cmd_SetName(GameObject go){
		networkMgr = GameObject.Find ("PlayerInfoHandler");
		assignInfo = GetComponent<NetworkCharacterInfo> ();
		assignInfo.Rpc_SetName (go);
	}
	[Command]
	public void Cmd_KillYourself(GameObject go){
		networkMgr = GameObject.Find ("PlayerInfoHandler");
		assignInfo = GetComponent<NetworkCharacterInfo> ();
		assignInfo.Rpc_KillAPlayer(go);
	}
}
