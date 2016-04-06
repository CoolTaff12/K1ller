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

	public void KillAPlayer(GameObject go){
		Rpc_KillAPlayer (go);
	}

	[ClientRpc]
	public void Rpc_KillAPlayer(GameObject go){
		Animator anim = go.GetComponent<Animator> ();
		anim.enabled = false;
		foreach (Transform tr in go.GetComponent<GrabAndToss>().bodyparts) {
			tr.SetParent(null);
		}
	}
}
