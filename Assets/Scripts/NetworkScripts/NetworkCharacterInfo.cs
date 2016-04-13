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
    public int teamNumber;

    [SyncVar]
    public Texture playertexture;
    public Texture[] selectableTextures;
    [SyncVar]
    public int checkingTexture;

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
        //Renderar the colour and the texture player had choosen ealier
        Renderer[] CRends = GetComponentsInChildren<Renderer>();
        if(checkingTexture == 1)
        {
            playertexture = selectableTextures[0];
        }
        if (checkingTexture == 2)
        {
            playertexture = selectableTextures[1];
        }
        if (checkingTexture == 3)
        {
            playertexture = selectableTextures[2];
        }
        Color headcolor = GameObject.Find(this.gameObject.name + "/Body/regular_dude_body").GetComponent<Renderer>().material.color;
        foreach (Renderer r in CRends)
        {
            r.material.mainTexture = playertexture;
            r.material.color = color;
        }
        GameObject.Find(this.gameObject.name + "/Body/regular_dude_head").GetComponent<Renderer>().material.color = headcolor;
    }
	
	// Update is called once per frame
	void Update () {
        UnityEngine.Cursor.visible = true;
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
