using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class GrabAndToss : NetworkBehaviour
{

	RaycastHit hit;
	public float rayDistance;
	public float rayRadius;
	public float tossForce;
	[SyncVar]
	public int teamNumber;
	public int killed = 1;
	[SyncVar]
	public float health = 1f;
	public bool killable = true;
	public bool dead = false;
	public bool holdingBall;
	public Transform[] Bodyparts;
	public DodgeBallBehaviour ballInfo;
	public AssignPlayerInfo assignInfo;
	public DodgeBallBehaviour ballScript;
	public GameObject fpc;
	public GameObject head;
	public GameObject currentBall;
	public GameObject holdPos;
	public GameObject networkMgr;

	// Use this for initialization
	void Start ()
	{
		Cmd_SetTeamNumber (gameObject);

	}

	// Update is called once per frame
	void Update ()
	{
		if (health <= 0 && !dead) {
			//			RemoveChild ();
			dead = true;
			teamNumber = 0;
		}
		Debug.DrawRay (head.transform.position, head.transform.forward, Color.green, rayDistance);
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
		assignInfo = networkMgr.GetComponent<AssignPlayerInfo> ();
		Debug.Log (go + " = GameObject We Are Looking For");
		assignInfo.Rpc_SetTeamNumber (go);


	}
}
