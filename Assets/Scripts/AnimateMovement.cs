using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AnimateMovement : NetworkBehaviour {
	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
