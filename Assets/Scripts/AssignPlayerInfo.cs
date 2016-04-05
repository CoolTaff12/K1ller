using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AssignPlayerInfo : NetworkBehaviour {
	[SyncVar]
	public int teamNumber = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
//	public void setTeamNumber(GameObject go)
//	{
//		Rpc_SetTeamNumber(go);
//	}
}
