﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnOnServer : NetworkBehaviour {
	public GameObject prefabToSpawn;
	public string objectName;
	public bool onlyOne = false;

	// Use this for initialization
	void Start () {
//		objectName = gameObject.transform.name;
		if(!onlyOne || GameObject.Find(objectName) == null){
			GameObject gots = Instantiate(prefabToSpawn, transform.position, Quaternion.identity) as GameObject;
			CmdSpawnOnServer (gots, transform.position);
			gots.transform.name = objectName;
			gots.transform.SetParent (gameObject.transform);
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	[Command]
	public void CmdSpawnOnServer(GameObject go, Vector3 pos){
		NetworkServer.Spawn(go);
	}
}
