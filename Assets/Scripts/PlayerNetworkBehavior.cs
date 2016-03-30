using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkBehavior : NetworkBehaviour {

	public Camera cam;
	public GrabAndToss gat;
	public PlayerTarget playerTarget;
	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			gat = gameObject.GetComponent<GrabAndToss> ();
			playerTarget = gameObject.GetComponent <PlayerTarget> ();

			gat.enabled = true;
			playerTarget.enabled = true;
		} else {
			cam.enabled = false;
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
