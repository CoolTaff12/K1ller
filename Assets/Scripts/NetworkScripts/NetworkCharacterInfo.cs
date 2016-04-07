using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;



[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class NetworkCharacterInfo : NetworkBehaviour
{

    //Network syncvar
   [SyncVar(hook = "OnScoreChanged")]
    public int score;
    [SyncVar]
    public Color color;
    [SyncVar]
    public string playerName;
    [SyncVar(hook = "OnLifeChanged")]
    public int lifeCount;
    [SyncVar]
    public int teamNumber = 1;

    protected Text ScoreText;
    //hard to control WHEN Init is called (networking make order between object spawning non deterministic)
    //so we call init from multiple location (depending on what between spaceship & manager is created first).
    protected bool WasInit = false;


    void Awake()
    {
        //register the spaceship in the gamemanager, that will allow to loop on it.
        NetworkGameManager.sCHaracter.Add(this);
    }

    // Use this for initialization
    void Start ()
    {
        name = playerName;
        Renderer[] CRends = GetComponentsInChildren<Renderer>();
        Renderer TshirtRends = GameObject.Find(this.gameObject.name + "/Body/regular_dude_body").GetComponent<Renderer>();
        TshirtRends.material.color = color;
      /*  foreach (Renderer r in CRends)
        {
            r.material.color = color;
        }*/
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    [ClientRpc]
    public void Rpc_SetTeamNumber(GameObject go)
    {
        go.GetComponent<GrabAndToss>().teamNumber = teamNumber;
        teamNumber++;
    }
    [ClientRpc]
    public void Rpc_SetName(GameObject go)
    {
        go.transform.name = "Player" + teamNumber;
    }
    [ClientRpc]
    public void Rpc_KillAPlayer(GameObject go)
    {
        go.GetComponent<GrabAndToss>().dead = true;
        go.GetComponent<GrabAndToss>().teamNumber = 0;
        go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.detectCollisions = false;
        rb.useGravity = false;
    }

    public void Init()
    {
        if (WasInit)
            return;

        GameObject scoreGO = new GameObject(playerName + "score");
        scoreGO.transform.SetParent(NetworkGameManager.sInstance.uiScoreZone.transform, false);
        ScoreText = scoreGO.AddComponent<Text>();
        ScoreText.alignment = TextAnchor.MiddleCenter;
        ScoreText.font = NetworkGameManager.sInstance.uiScoreFont;
        ScoreText.resizeTextForBestFit = true;
        ScoreText.color = color;
        WasInit = true;

        UpdateScoreLifeText();
    }
    // --- Score & Life management & display
    void OnScoreChanged(int newValue)
    {
        score = newValue;
        UpdateScoreLifeText();
    }

    void OnLifeChanged(int newValue)
    {
        lifeCount = newValue;
        UpdateScoreLifeText();
    }

    void UpdateScoreLifeText()
    {
        if (ScoreText != null)
        {
            ScoreText.text = playerName + "\nSCORE : " + score + "\nLIFE : ";
            for (int i = 1; i <= lifeCount; ++i)
                ScoreText.text += "X";
        }
    }
}
