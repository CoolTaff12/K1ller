using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkBehavior : NetworkBehaviour {

	[SerializeField] Camera cam = null;
	[SerializeField] GrabAndToss gat;
	[SerializeField] AudioListener audioLis = null;
	[SerializeField] GameObject[] bodyParts = null;


	public override void OnStartLocalPlayer ()
	{
		cam.enabled = true;
		gat = gameObject.GetComponent<GrabAndToss> ();
		gat.enabled = true;
		audioLis.enabled = true;
		foreach (GameObject go in bodyParts) {
							go.layer = 9;
						}
		//Enable network animations
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
		
}
