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
		go.GetComponent<PlayerInfo>().dead = true;
		go.GetComponent<NetworkCharacterInfo>().teamNumber = 0;
		go.GetComponent<BoxCollider> ().enabled = false;

		go.layer = 10;

		go.GetComponent<FirstPersonController> ().m_RunSpeed = 30;
		go.GetComponent<FirstPersonController> ().m_WalkSpeed = 15;
		go.GetComponent<FirstPersonController> ().m_JumpSpeed = 0;
		go.GetComponent<FirstPersonController> ().m_GravityMultiplier = 0;
		Rigidbody rb = go.GetComponent<Rigidbody>();
		rb.detectCollisions = false;
		rb.useGravity = false;
		rb.Sleep ();
		go.GetComponent<PlayerInfo>().deathMessage.SetActive (true);
		go.GetComponent<PlayerInfo> ().body.SetActive (false);
		foreach(GameObject gos in go.GetComponent<PlayerInfo> ().bodyparts){
			gos.GetComponent<Renderer> ().material.mainTexture = go.GetComponent<PlayerInfo> ().mat;
						}
	}
	[ClientRpc]
	public void Rpc_SpawnHead(GameObject go)
	{
		GameObject HeadBall = Instantiate(go.GetComponent<PlayerInfo>().ballPrefab, go.GetComponent<GrabAndToss>().head.transform.position, Quaternion.identity) as GameObject;
		HeadBall.GetComponent<Renderer> ().material.mainTexture = go.GetComponent<PlayerInfo>().bodyparts [0].GetComponent<Renderer> ().material.mainTexture;
		NetworkServer.Spawn(go.GetComponent<PlayerInfo>().ballPrefab);
	}

	[ClientRpc]
	public void Rpc_DealDamage(GameObject go)
	{
		go.GetComponent<PlayerInfo> ().health -= 1;
	}

}
