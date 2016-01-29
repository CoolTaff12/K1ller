using UnityEngine;
using System.Collections;

public class PlayerTarget : MonoBehaviour {
	public int teamNumber;
	public float health = 1f;
	public bool killable = true;
	public Transform[] Bodyparts;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			RemoveChild ();
		}
	
	}
	public void RemoveChild(){
		foreach (Transform bpart in Bodyparts){
//			Destroy (bpart.gameObject);
			Rigidbody rb = bpart.GetComponent<Rigidbody> ();
			bpart.parent = null;
			rb.isKinematic = false;  	
		}
	}
}
