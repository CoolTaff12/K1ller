using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class AssignPlayerInfo : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	[ClientRpc]
	public void Rpc_KillAPlayer(GameObject go)
	{
		go.GetComponent<GrabAndToss>().dead = true;
		go.GetComponent<GrabAndToss>().teamNumber = 0;
//		go.GetComponent<FirstPersonController> ().m_RunSpeed = 0;
//		go.GetComponent<FirstPersonController> ().m_WalkSpeed = 0;
		go.GetComponent<FirstPersonController> ().m_JumpSpeed = 0;
		go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		Rigidbody rb = go.GetComponent<Rigidbody>();
		rb.detectCollisions = false;
		rb.useGravity = false;
	}
}
