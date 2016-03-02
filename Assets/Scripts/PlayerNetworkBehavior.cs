using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkBehavior : NetworkBehaviour {

	public Camera cam;
	// Use this for initialization
	void Start () {
		if (isLocalPlayer) return;

		cam.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
