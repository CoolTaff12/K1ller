using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerInfo : NetworkBehaviour {
	[SerializeField]
	private GameObject[] bodyparts = null; //List of bodypart segments.
	[SerializeField]
	private GameObject deathMessage = null;
	private GameObject infoHandler; //PlayerInfoHandler found in scene.
	[SerializeField]
	private GameObject ballPrefab = null;
	[SyncVar][SerializeField]
	private GameObject body;
	[SyncVar][SerializeField]
	private float health = 1f; //Amount of hits the character can take before dying.
	[SerializeField]
	private float flySpeed = 4f;
	[SyncVar][SerializeField]
	private bool killable = true; //Can this character be killed?
	[SyncVar][SerializeField]
	private bool dead = false; //Is this character dead?
	public bool c_Dead {get{return dead;}}
	[SerializeField]
	private Texture mat = null;
	[SerializeField]
	private AudioClip[] audioClips = null;
	private AssignPlayerInfo assignInfo; //Script to set initial info such as teamNumber.
	private NetworkLobbyHook NLH;
	private GrabAndToss gat;
	private NetworkCharacterInfo netInfo;
	private DodgeBallBehaviour ballInfo; //Script on the ball colliding with the player;

	// Use this for initialization
	void Start () {
		gat = gameObject.GetComponent<GrabAndToss> ();
		NLH = GameObject.Find("LobbyManager").GetComponent<NetworkLobbyHook>();
		netInfo = gameObject.GetComponent<NetworkCharacterInfo> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0 && !c_Dead) {
			dead = true;
			Debug.Log ("GetDead");
				Cmd_SpawnHead(gameObject);
				Cmd_KillYourself(gameObject);
			Debug.Log ("SpawnHead");
				if (gat.c_HoldingBall) {
					gat.c_TossForce = 1f;
					gat.Cmd_Shoot (gat.c_CurrentBall, gat.c_TossForce);
			}
		}
		if (dead && isLocalPlayer) {
			if (Input.GetKey (KeyCode.Space)) {
				gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y + flySpeed *Time.deltaTime , transform.position.z);
			}
			if (Input.GetKey (KeyCode.LeftControl)) {
				gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y - flySpeed * Time.deltaTime , transform.position.z);
			}
		}
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ball")
		{
			ballInfo = col.gameObject.GetComponent<DodgeBallBehaviour>();
			if (netInfo.teamNumber != ballInfo.b_ThrownByTeam && ballInfo.b_ThrownByTeam != 0 && killable) {
				Cmd_TakeDamage (gameObject);
			}
		}
	}

	//-----------------Play Audio------------------------
	//This will take the gameobjects AudioSource to switch the audioclips
	public void PlaySound(int clip)
	{
		GetComponent<AudioSource>().clip = audioClips[clip];
		GetComponent<AudioSource>().Play();
	}

/// <summary>
/// Command to spawn a ball to simulate the player dropping their head.
/// </summary>
/// <param name="go">Character droppping the head : self</param>
	[Command]
	void Cmd_SpawnHead(GameObject go){
		infoHandler = GameObject.Find ("PlayerInfoHandler");
		assignInfo = infoHandler.GetComponent<AssignPlayerInfo> ();
		assignInfo.Cmd_SpawnHead(ballPrefab, gat.c_Head);
	}
	/// <summary>
	/// Command that kills the character.
	/// </summary>
	/// <param name="go">Character dying : self</param>
	[Command]
	public void Cmd_KillYourself(GameObject go){
		infoHandler = GameObject.Find ("PlayerInfoHandler");
		assignInfo = infoHandler.GetComponent<AssignPlayerInfo> ();
		assignInfo.Cmd_KillAPlayer(go);
	}
	/// <summary>
	/// Command to take damage, starts Rpc_TakeDamage.
	/// </summary>
	/// <param name="go">Character taking damage : self</param>
	[Command]
	public void Cmd_TakeDamage(GameObject go) {
		Rpc_TakeDamage (go, 1);
	}
/// <summary>
/// Rpc that decreases health.
/// </summary>
/// <param name="go">Character taking damage : self</param>
/// <param name="i">Damage taken</param>
	[ClientRpc]
	public void Rpc_TakeDamage(GameObject go, int i){
		health -= i;
	}

	/// <summary>
	/// Rpc that sets all the values when dying and call all players lists that it have i
	/// </summary>
	[ClientRpc]
	public void Rpc_KillYourself()
	{
        GameObject[] CharactersInfo = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject NCI in CharactersInfo)
        {
            NCI.GetComponent<NetworkCharacterInfo>().Rpc_CheckingList(gameObject);
            Debug.Log("Players name " + NCI.name);
        }
        //  gameObject.GetComponent<PlayerInfo>().dead = true;
        gameObject.GetComponent<BoxCollider> ().enabled = false;


		gameObject.GetComponent<FirstPersonController> ().m_RunSpeed = 30;
		gameObject.GetComponent<FirstPersonController> ().m_WalkSpeed = 15;
		gameObject.GetComponent<FirstPersonController> ().m_JumpSpeed = 0;
		gameObject.GetComponent<FirstPersonController> ().m_GravityMultiplier = 0;
		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		rb.detectCollisions = false;
		rb.useGravity = false;
		rb.Sleep ();
		deathMessage.SetActive (true);
		foreach(GameObject gos in bodyparts){
			gos.GetComponent<Renderer> ().material.mainTexture = mat;
			gos.layer = 10;
		}
        gameObject.layer = 10;
	}
}