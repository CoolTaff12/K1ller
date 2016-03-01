using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {
	Rigidbody rb;
	public float speed = 2;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			rb.velocity = new Vector3 (rb.velocity.y, rb.velocity.y, speed);
		}
		if (Input.GetKey (KeyCode.S)) {
			rb.velocity = new Vector3 (rb.velocity.y, rb.velocity.y, -speed);
		}
		if (Input.GetKey (KeyCode.A)) {
			rb.velocity = new Vector3 (speed, rb.velocity.y, rb.velocity.z);
		}
		if (Input.GetKey (KeyCode.D)) {
			rb.velocity = new Vector3 (-speed, rb.velocity.y, rb.velocity.z);
		}
	
	}
}
