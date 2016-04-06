using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class AnimateMovement : NetworkBehaviour {
	public Animator anim;
	private FirstPersonController Fpc;
	public GrabAndToss gat;
	private bool jumpPlayed = false;
	private bool throwPlayed = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		Fpc = gameObject.GetComponent<FirstPersonController> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMoveAnim ();
	}
	void UpdateMoveAnim(){
		if (isLocalPlayer) {
			if (Input.GetButton("Vertical"))
			{
				anim.SetBool("isJogging", true);
				anim.SetBool("isIdle", false);
			}
			if (Input.GetButtonUp("Vertical"))
			{
				anim.SetBool("isJogging", false);
				anim.SetBool("isIdle", true);
			}
			if (Fpc.m_Jumping && !jumpPlayed) {
				anim.SetBool("isJumping", true);
				jumpPlayed = true;
			}
			if (!Fpc.m_Jumping && jumpPlayed) {
				jumpPlayed = false;
				anim.SetBool("isJumping", false);
				anim.SetBool("isIdle", true);
			}
//			if (gat.throwing && !throwPlayed) {
//				anim.SetBool ("isThrowing", true);
//				throwPlayed = true;
//			}
//				if (!gat.throwing && throwPlayed) {
//					anim.SetBool ("isThrowing", false);
//				throwPlayed = false;
//			}

//			} else if (!gameObject.GetComponent<GrabAndToss> ().throwing){
//							if (anim.GetNextAnimatorStateInfo (0).IsName ("isThrowing")) {
//								anim.SetBool ("isThrowing", false);
//							}
//			}

	}
		
}
}
