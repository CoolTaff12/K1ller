using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerTarget : NetworkBehaviour {
	public int teamNumber;
	public float health = 1f;
    public int killed = 1;
	public bool killable = true;
	public bool dead = false;
	public Transform[] Bodyparts;
	public DodgeBallBehaviour ballInfo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0 && !dead) {
//			RemoveChild ();
			dead = true;
			teamNumber = 0;
			gameObject.GetComponent<GrabAndToss>().teamNumber = teamNumber;
		}
	}
//	public void RemoveChild(){
//		foreach (Transform bpart in Bodyparts){
//			Rigidbody rb = bpart.GetComponent<Rigidbody> ();
//			bpart.parent = null;
//			rb.isKinematic = false;  	
//		}
//        if(killed == 1)
//        {
//            Bodyparts[0].gameObject.AddComponent<AudioSource>();
//            Bodyparts[0].gameObject.AddComponent<DodgeBallBehaviour>();
//            Bodyparts[0].gameObject.GetComponent<DodgeBallBehaviour>().playerInfo = null;
//            killed = 0;
//        }
//    }
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ball")
		{
			ballInfo = col.gameObject.GetComponent<DodgeBallBehaviour>();
			if (teamNumber != thrownByTeam) {
				playerInfo.health -= 1;
			}
}