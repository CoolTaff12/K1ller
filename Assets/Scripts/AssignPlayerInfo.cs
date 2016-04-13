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
		
	[Command]
	public void Cmd_KillAPlayer(GameObject go)
	{
		go.GetComponent<PlayerInfo> ().Rpc_KillYourself ();
	}
	[Command]
	public void Cmd_SpawnHead(GameObject go)
	{
		GameObject HeadBall = Instantiate(go.GetComponent<PlayerInfo>().ballPrefab, go.GetComponent<GrabAndToss>().head.transform.position, Quaternion.identity) as GameObject;
//		HeadBall.GetComponent<Renderer> ().material.mainTexture = go.GetComponent<PlayerInfo>().bodyparts [0].GetComponent<Renderer> ().material.mainTexture;
		NetworkServer.Spawn(HeadBall);
	}
}
