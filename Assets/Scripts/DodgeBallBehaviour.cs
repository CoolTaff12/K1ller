using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DodgeBallBehaviour : NetworkBehaviour {
	public AudioClip[] audioClips = new AudioClip[1];
    public AudioClip[] HitAudioClips = new AudioClip[4];
	public PlayerTarget playerInfo;
	public GrabAndToss gat;
	public Rigidbody rb;
	public Collider coll;
	[SyncVar]
	public int thrownByTeam = 1;
	public GameObject Sparks;
	[SerializeField]
	private Transform myTransform;
	public bool pickedUp;
	public GameObject currentPlayer;

	// Use this for initialization
	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		coll = gameObject.GetComponent<SphereCollider> ();
		if (!isLocalPlayer) {
			return;
		}
			audioClips[0] = Resources.Load("Sound/Basketball-BallBounce") as AudioClip;
			Sparks = Resources.Load("Particles/child prefabs/enmy Death") as GameObject;
		}

	// Update is called once per frame;
	void Update ()
	{
		if (pickedUp) {
			gameObject.transform.position = gat.holdPos.transform.position;
			gameObject.transform.rotation = gat.fpc.transform.rotation;
		}

	}
	//-----------------Play Audio------------------------
	//This will take the gameobjects AudioSource to switch the audioclips
	public void PlaySound(int clip)
	{
		GetComponent<AudioSource>().clip = audioClips[clip];
		GetComponent<AudioSource>().Play();
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "ForceField")
		{
			GameObject Sparked = (GameObject) Instantiate(Sparks, transform.position, Quaternion.identity);
			Destroy(Sparked, 3f);
		}
		else if (col.gameObject.tag != "Player" || col.gameObject.tag != "ForceField")
		{
			PlaySound(0);
		}

	}
	[ClientRpc]
	public void Rpc_GetPickedUp (GameObject go){
		coll.enabled = false;
		rb.detectCollisions = false;
		rb.isKinematic = true;
		currentPlayer = go;
		gat = currentPlayer.GetComponent<GrabAndToss> ();
		pickedUp = true;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	
	}
	[ClientRpc]
	public void Rpc_Shoot (){
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.isKinematic = false;
		thrownByTeam = gat.teamNumber;
		coll.enabled = true;
		rb.detectCollisions = true;
		rb.AddForce(gat.head.transform.forward * gat.tossForce);
		gat = null;
		pickedUp = false;
	}
//	public void GetPickedUp(GameObject go){
//		Rpc_GetPickedUp (go);
//	}
//	public void Shoot(){
//	Rpc_Shoot();
//}

}
