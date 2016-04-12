using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerInfo : NetworkBehaviour {
	public GameObject[] bodyparts; //List of bodypart segments.
	public GameObject deathMessage;
	public GameObject infoHandler; //PlayerInfoHandler found in scene.
	public GameObject ballPrefab;
	[SyncVar]
	public GameObject body;
	[SyncVar]
	public float health = 1f; //Amount of hits the character can take before dying.
	[SyncVar]
	public bool killable = true; //Can this character be killed?
	[SyncVar]
	public bool dead = false; //Is this character dead?
	public Texture mat;
	public AudioClip[] audioClips;
	public AssignPlayerInfo assignInfo; //Script to set initial info such as teamNumber.
	public NetworkLobbyHook NLH;
	public GrabAndToss gat;
	public NetworkCharacterInfo netInfo;
	public DodgeBallBehaviour ballInfo; //Script on the ball colliding with the player;

	// Use this for initialization
	void Start () {
		gat = gameObject.GetComponent<GrabAndToss> ();
		NLH = GameObject.Find("LobbyManager").GetComponent<NetworkLobbyHook>();
		netInfo = gameObject.GetComponent<NetworkCharacterInfo> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0 && !dead) {
			Cmd_SpawnHead(gameObject);
			//			KillYourSelf();
			Cmd_KillYourself(gameObject);
			dead = true;
			netInfo.teamNumber = 0;
			if (gat.holdingBall) {
				gat.tossForce = 1f;
				gat.Cmd_Shoot (gat.currentBall);
			}
		}
		if (dead && isLocalPlayer) {
			if (Input.GetKey (KeyCode.Space)) {
				gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z);
			}
			if (Input.GetKey (KeyCode.LeftControl)) {
				gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y - 0.1f, transform.position.z);
			}
		}
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ball")
		{
			ballInfo = col.gameObject.GetComponent<DodgeBallBehaviour>();
			if (netInfo.teamNumber != ballInfo.thrownByTeam && ballInfo.thrownByTeam != 0) {
				Cmd_TakeDamage (gameObject);
			}
		}
	}
	void KillYourSelf(){
		dead = true;
		NLH.CheackingList(this.gameObject);
		int SelectSoundFile = Random.Range(0, 2);
		PlaySound(SelectSoundFile);
		GetComponent<BoxCollider> ().enabled = false;
		bodyparts[8].layer = 9;
		bodyparts [9].layer = 9;
		gameObject.layer = 10;

		GetComponent<FirstPersonController> ().m_RunSpeed = 30;
		GetComponent<FirstPersonController> ().m_WalkSpeed = 15;
		GetComponent<FirstPersonController> ().m_JumpSpeed = 0;
		GetComponent<FirstPersonController> ().m_GravityMultiplier = 0;
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.detectCollisions = false;
		rb.useGravity = false;
		rb.Sleep ();
		deathMessage.SetActive (true);
		foreach(GameObject gos in bodyparts){
			gos.GetComponent<Renderer> ().material.mainTexture = mat;
		}
	}
	//-----------------Play Audio------------------------
	//This will take the gameobjects AudioSource to switch the audioclips
	public void PlaySound(int clip)
	{
		GetComponent<AudioSource>().clip = audioClips[clip];
		GetComponent<AudioSource>().Play();
	}
	[Command]
	void Cmd_SpawnHead(GameObject go){
		infoHandler = GameObject.Find ("PlayerInfoHandler");
		assignInfo = infoHandler.GetComponent<AssignPlayerInfo> ();
		assignInfo.Rpc_SpawnHead(go);
//		GameObject HeadBall = Instantiate(ballPrefab, go.GetComponent<GrabAndToss>().head.transform.position, Quaternion.identity) as GameObject;
//		HeadBall.GetComponent<Renderer> ().material.mainTexture = bodyparts [0].GetComponent<Renderer> ().material.mainTexture;
//		NetworkServer.Spawn(ballPrefab);
	}
	[Command]
	public void Cmd_KillYourself(GameObject go){
		infoHandler = GameObject.Find ("PlayerInfoHandler");
		assignInfo = infoHandler.GetComponent<AssignPlayerInfo> ();
		assignInfo.Rpc_KillAPlayer(go);
		KillYourSelf();
//				dead = true;
//				NLH.CheackingList(this.gameObject);
//				int SelectSoundFile = Random.Range(0, 2);
//				PlaySound(SelectSoundFile);
//				GetComponent<BoxCollider> ().enabled = false;
//				bodyparts[8].layer = 9;
//				bodyparts [9].layer = 9;
//				gameObject.layer = 10;
//		
//				GetComponent<FirstPersonController> ().m_RunSpeed = 30;
//				GetComponent<FirstPersonController> ().m_WalkSpeed = 15;
//				GetComponent<FirstPersonController> ().m_JumpSpeed = 0;
//				GetComponent<FirstPersonController> ().m_GravityMultiplier = 0;
//				Rigidbody rb = GetComponent<Rigidbody>();
//				rb.detectCollisions = false;
//				rb.useGravity = false;
//				rb.Sleep ();
//				deathMessage.SetActive (true);
//				body.SetActive (false);
//				foreach(GameObject gos in bodyparts){
//					Network.Destroy(gos);
//				}
	}
	[Command]
	public void Cmd_TakeDamage(GameObject go) {
//		health--;
		infoHandler = GameObject.Find ("PlayerInfoHandler");
		assignInfo = infoHandler.GetComponent<AssignPlayerInfo> ();
		assignInfo.Rpc_DealDamage(go);
	}
}