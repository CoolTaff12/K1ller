using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	/// <summary>
	/// Request spawning of a ball when a player character dies.
	/// </summary>
	/// <param name="go">Prefab of the object that should spawn.</param>
	/// <param name="pos">spawnposition</param>
	[Command]
	public void Cmd_SpawnHead(GameObject go, GameObject pos)
	{
		GameObject HeadBall = Instantiate(go, pos.transform.position, Quaternion.identity) as GameObject;
		NetworkServer.Spawn(HeadBall);
	}
}
