using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkBehavior : NetworkBehaviour {

	[SerializeField] Camera cam;
	[SerializeField] GrabAndToss gat;
	[SerializeField] AudioListener audioLis;
	[SerializeField] GameObject[] bodyParts;


	public override void OnStartLocalPlayer ()
	{
		cam.enabled = true;
		gat = gameObject.GetComponent<GrabAndToss> ();
		gat.enabled = true;
		audioLis.enabled = true;
		foreach (GameObject go in bodyParts) {
							go.layer = 9;
						}

		GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
		GetComponent<NetworkAnimator> ().SetParameterAutoSend (1, true);
		GetComponent<NetworkAnimator> ().SetParameterAutoSend (2, true);
		GetComponent<NetworkAnimator> ().SetParameterAutoSend (3, true);
		GetComponent<NetworkAnimator> ().SetParameterAutoSend (4, true);
	}

	public override void PreStartClient(){
		GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
		GetComponent<NetworkAnimator> ().SetParameterAutoSend (1, true);
		GetComponent<NetworkAnimator> ().SetParameterAutoSend (2, true);
		GetComponent<NetworkAnimator> ().SetParameterAutoSend (3, true);
		GetComponent<NetworkAnimator> ().SetParameterAutoSend (4, true);

	}

//	public PlayerTarget playerTarget;
	// Use this for initialization
//	void Start () {
//		if (isLocalPlayer) {
//			gat = gameObject.GetComponent<GrabAndToss> ();
////			playerTarget = gameObject.GetComponent <PlayerTarget> ();
//			gat.enabled = true;
//			foreach (GameObject go in bodyParts) {
//				go.layer = 9;
//			}
////			playerTarget.enabled = true;
//		} else {
//			cam.enabled = false;
//			return;
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
}
