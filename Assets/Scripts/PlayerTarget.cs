using UnityEngine;
using System.Collections;

public class PlayerTarget : MonoBehaviour {
	public int teamNumber;
	public float health = 1f;
	public bool killable = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy (gameObject);
		}
	
	}
}
