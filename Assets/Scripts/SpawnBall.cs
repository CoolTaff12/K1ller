using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnBall : NetworkBehaviour {
	public GameObject ballPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider col){
		if (col.tag == "Player"){
			GameObject SpeedingBall = Instantiate(ballPrefab, transform.position, Quaternion.identity) as GameObject;
			CmdSpawnOnServer(SpeedingBall);	
		}
	}
	/// <summary>
	/// Spawns an object on the server.
	/// </summary>
	/// <param name="go">GameObject that will be spawned.</param>
	[Command]
	public void CmdSpawnOnServer(GameObject go){
		NetworkServer.Spawn(go);
	}
}
