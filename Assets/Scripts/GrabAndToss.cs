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
	public int teamNumber = 0; //Number of the team this character is on. Is set by AssignPlayerInfo Script.
//	public int killed = 1; //Is this character killed? 1 for true, 0 for false;
	[SyncVar]
	public float health = 1f; //Amount of hits the character can take before dying.
	[SyncVar]
	public bool killable = true; //Can this character be killed?
	[SyncVar]
	public bool dead = false; //Is this character dead?
	[SyncVar]
	public bool holdingBall; //Is this character holding a ball?
	public bool throwing = false;
	public GameObject[] bodyparts; //List of bodypart segments.
	public DodgeBallBehaviour ballInfo; //Script on the ball colliding with the player;
	public DodgeBallBehaviour ballScript; //Script on the ball hit by the players' raycast.
	public AssignPlayerInfo assignInfo; //Script to set initial info such as teamNumber.
	public Animator anim;//Animator attached to the player.
	public GameObject fpc; //FirstPersonController connected to the player.
	public GameObject head; //Head of the player;
	public GameObject currentBall; //Ball that is currently being held.
	public GameObject holdPos; //Position of the held ball.
	public GameObject networkMgr; //NetworkManager found in scene.
	public GameObject ballPrefab;
	public GameObject deathMessage;
	[SyncVar]
	public GameObject body;

	// Use this for initialization
	void Start ()
	{
//		foreach(GameObject go in bodyparts){
//			go.GetComponent<Rigidbody> ().isKinematic = true;
//			go.GetComponent<Rigidbody> ().detectCollisions = false;
//			go.GetComponent<Rigidbody> ().useGravity = false;
//		}
		anim = GetComponent<Animator>();
		teamNumber = GetComponent<NetworkCharacterInfo> ().teamNumber;


	}

	// Update is called once per frame
	void Update ()
	{
		if (health <= 0 && !dead) {
			Cmd_SpawnHead(gameObject);
//			KillYourSelf();
			Cmd_KillYourself(gameObject);
			dead = true;
			teamNumber = 0;
			if (holdingBall) {
				tossForce = 1f;
				Cmd_Shoot (currentBall);
			}
		}
		if (dead) {
			if (Input.GetKey (KeyCode.Space)) {
				gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z);
			}
			if (Input.GetKey (KeyCode.LeftControl)) {
				gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y - 0.1f, transform.position.z);
			}
		}

//DEBUG//
Debug.DrawRay (head.transform.position, head.transform.forward, Color.green, rayDistance);
		if (Physics.SphereCast (head.transform.position, rayRadius, head.transform.forward, out hit, rayDistance)) {
			if (!isLocalPlayer) {
				return;
			}
			if (hit.collider.GetComponent<DodgeBallBehaviour> () != null) {
				print ("Ball!");
				if (Input.GetKey (KeyCode.E) || CrossPlatformInputManager.GetButton ("Fire1") && !holdingBall && !dead) {
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
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ball")
		{
			ballInfo = col.gameObject.GetComponent<DodgeBallBehaviour>();
			if (teamNumber != ballInfo.thrownByTeam && ballInfo.thrownByTeam != 0) {
				Cmd_TakeDamage (gameObject);
			}
		}
	}

	void KillYourSelf(){
		dead = true;
		teamNumber = 0;
		GetComponent<BoxCollider> ().enabled = false;
		bodyparts[8].layer = 9;
		bodyparts [9].layer = 9;
//		foreach(GameObject go in bodyparts){
//			go.transform.SetParent (null);
//			go.GetComponent<Rigidbody> ().isKinematic = false;
//			go.GetComponent<Rigidbody> ().detectCollisions = true;
//			go.GetComponent<Rigidbody> ().useGravity = true;
//		}
		gameObject.layer = 10;

		GetComponent<FirstPersonController> ().m_RunSpeed = 30;
		GetComponent<FirstPersonController> ().m_WalkSpeed = 15;
		GetComponent<FirstPersonController> ().m_JumpSpeed = 0;
		GetComponent<FirstPersonController> ().m_GravityMultiplier = 0;
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.detectCollisions = false;
		rb.useGravity = false;
		rb.Sleep ();
		deathMessage.SetActive (true);
		body.SetActive (false);
		if (holdingBall) {
			tossForce = 1f;
			Cmd_Shoot (currentBall);
		}
	}
	[Command]
	public void Cmd_TakeDamage(GameObject go) {
		networkMgr = GameObject.Find ("PlayerInfoHandler");
		assignInfo = networkMgr.GetComponent<AssignPlayerInfo> ();
		assignInfo.Rpc_DealDamage(go);
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
	void Cmd_SpawnHead(GameObject go){
		networkMgr = GameObject.Find ("PlayerInfoHandler");
		assignInfo = networkMgr.GetComponent<AssignPlayerInfo> ();
		assignInfo.Rpc_SpawnHead(go);
	}
	[Command]
	public void Cmd_KillYourself(GameObject go){
		networkMgr = GameObject.Find ("PlayerInfoHandler");
		assignInfo = networkMgr.GetComponent<AssignPlayerInfo> ();
		assignInfo.Rpc_KillAPlayer(go);
	}
	IEnumerator StartThrow(float waitTime) 
	{
		yield return new WaitForSeconds(waitTime);
	}
}
