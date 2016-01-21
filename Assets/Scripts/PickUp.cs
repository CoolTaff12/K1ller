using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {
	RaycastHit hit;
	public float rayDistance;
	public float rayRadius;
	public GameObject ParentFPC;
	public CrabandToss crab;

	// Use this for initialization
	void Start () {
		crab = ParentFPC.GetComponent<CrabandToss> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Physics.SphereCast (transform.position, rayRadius,  transform.forward, out hit, rayDistance)) {
			if(hit.collider.GetComponent<DodgeBallScript>() != null){
				print ("Ball!");
				if(Input.GetKeyDown(KeyCode.E)){
					crab.GrabBall.SetActive(true);
					Destroy(hit.collider.gameObject);
					crab.GotTheBall = true;

				}
			}
		}
		

}
}
