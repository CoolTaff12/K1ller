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
	[ClientRpc]
	public void Rpc_SetTeamNumber(GameObject go){
		go.GetComponent<GrabAndToss> ().teamNumber = teamNumber;
		teamNumber++;
	}
	[ClientRpc]
	public void Rpc_SetName(GameObject go){
		go.transform.name = "Player" + teamNumber;
	}
	[ClientRpc]
	public void Rpc_KillAPlayer(GameObject go){
		go.GetComponent<GrabAndToss> ().dead = true;
		go.GetComponent<GrabAndToss> ().teamNumber = 0;
		go.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
		Rigidbody rb = go.GetComponent<Rigidbody> ();
		rb.detectCollisions = false;
		rb.useGravity = false;
		
	}
}
