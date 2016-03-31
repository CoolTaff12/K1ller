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
	public bool holdingBall;
	public GameObject fpc;
	public GameObject head;
	public GameObject currentBall;
	public GameObject holdPos;
	public DodgeBallBehaviour ballScript;


    // Use this for initialization
    void Start ()
	{
      
    }

	// Update is called once per frame
	void Update ()
	{
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
}
