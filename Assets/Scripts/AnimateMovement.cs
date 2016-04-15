using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class AnimateMovement : NetworkBehaviour {
	private Animator anim;
	private FirstPersonController Fpc;
	private GrabAndToss gat;
	private bool jumpPlayed = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		Fpc = gameObject.GetComponent<FirstPersonController> ();
		gat = gameObject.GetComponent<GrabAndToss> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMoveAnim ();
	}
	/// <summary>
	/// Check if an animation should be playing and play it.
	/// </summary>
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

	}
		
}
}
