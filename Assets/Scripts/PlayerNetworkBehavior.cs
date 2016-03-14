using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkBehavior : NetworkBehaviour {

	public Camera cam;
	public CrabandToss cat;
	public PlayerTarget playerTarget;
	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) return;

		cat = gameObject.GetComponent<CrabandToss> ();
		playerTarget = gameObject.GetComponent <PlayerTarget> ();

		cam.enabled = true;
		cat.enabled = true;
		playerTarget.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
