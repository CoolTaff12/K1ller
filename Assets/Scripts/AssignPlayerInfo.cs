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
		go.GetComponent<BoxCollider> ().enabled = false;
		//		foreach(GameObject go in bodyparts){
		//			go.transform.SetParent (null);
		//			go.GetComponent<Rigidbody> ().isKinematic = false;
		//			go.GetComponent<Rigidbody> ().detectCollisions = true;
		//			go.GetComponent<Rigidbody> ().useGravity = true;
		//		}
		go.layer = 10;

		go.GetComponent<FirstPersonController> ().m_RunSpeed = 30;
		go.GetComponent<FirstPersonController> ().m_WalkSpeed = 15;
		go.GetComponent<FirstPersonController> ().m_JumpSpeed = 0;
		go.GetComponent<FirstPersonController> ().m_GravityMultiplier = 0;
		Rigidbody rb = go.GetComponent<Rigidbody>();
		rb.detectCollisions = false;
		rb.useGravity = false;
		rb.Sleep ();
		go.GetComponent<GrabAndToss>().deathMessage.SetActive (true);
		go.GetComponent<GrabAndToss> ().body.SetActive (false);

	}
	[ClientRpc]
	public void Rpc_SpawnHead(GameObject go)
	{
		GameObject HeadBall = Instantiate(go.GetComponent<GrabAndToss>().ballPrefab, go.GetComponent<GrabAndToss>().head.transform.position, Quaternion.identity) as GameObject;
		HeadBall.GetComponent<Renderer> ().material.mainTexture = go.GetComponent<GrabAndToss>().bodyparts [0].GetComponent<Renderer> ().material.mainTexture;
		NetworkServer.Spawn(go.GetComponent<GrabAndToss>().ballPrefab);
	}
	[ClientRpc]
	public void Rpc_DealDamage(GameObject go)
	{
		go.GetComponent<GrabAndToss> ().health -= 1;
	}

}
