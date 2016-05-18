using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;



[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class NetworkCharacterInfo : NetworkBehaviour
{

    //Network syncvar
    [SyncVar]
    public Color color;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public int teamNumber;

    [SyncVar]
    public Texture playertexture;
    public Texture[] selectableTextures;
    [SyncVar]
    public int checkingTexture;
    public int checkingPlayers;

    public int[] Teams;
    public List<GameObject> Team1;
    public List<GameObject> Team2;
    public List<GameObject> Team3;
    public List<GameObject> Team4;
    public List<GameObject> Team5;
    public List<GameObject> Team6;
    public List<GameObject> Team7;
    public List<GameObject> Team8;
    public List<GameObject> Team9;
    public List<GameObject> Team10;
    [SerializeField]
    private GameObject Victory;
    [SerializeField]
    private List<GameObject> TeamPlayers;

    public NetworkLobbyHook NLH;
    public UnityStandardAssets.Network.LobbyManager LM;

    public AudioClip[] audioClips;

    //hard to control WHEN Init is called (networking make order between object spawning non deterministic)
    //so we call init from multiple location (depending on what between spaceship & manager is created first).
    protected bool WasInit = false;

    protected bool CheckedPlayers = false;

    //-----------------Play Audio------------------------
    //This will take the gameobjects AudioSource to switch the audioclips
    public void PlaySound(int clip)
    {
        gameObject.transform.FindChild("FirstPersonCharacter").GetComponent<AudioSource>().clip = audioClips[clip];
        gameObject.transform.FindChild("FirstPersonCharacter").GetComponent<AudioSource>().Play();
    }

    void Awake()
    {
        Teams = new int[10];
        Team1 = new List<GameObject>();
        Team2 = new List<GameObject>();
        Team3 = new List<GameObject>();
        Team4 = new List<GameObject>();
        Team5 = new List<GameObject>();
        Team6 = new List<GameObject>();
        Team7 = new List<GameObject>();
        Team8 = new List<GameObject>();
        Team9 = new List<GameObject>();
        Team10 = new List<GameObject>();
        TeamPlayers = new List<GameObject>();
    }

    // Use this for initialization
    void Start ()
    {
        NLH = GameObject.Find("LobbyManager").GetComponent<NetworkLobbyHook>();
        LM = GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>();
        checkingPlayers = LM.PlayersOnline.Count;
        //Tells the player what its name is
        gameObject.name = playerName;
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
        //Two Materials
        /*     GameObject.Find(this.gameObject.name + "/Body/regular_dude_right_hand").GetComponent<Renderer>().material.color = headcolor;
             GameObject.Find(this.gameObject.name + "/Body/regular_dude_right_hand").GetComponent<Renderer>().material.color = headcolor;*/
    }

    void Update()
    {
        if(checkingPlayers > 0)
        {
            GameObject[] AvalibleEntries = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject NewPlayer in AvalibleEntries)
            {
                if(TeamPlayers.Contains(NewPlayer))
                {

                }
                else
                {
                    TeamPlayers.Add(NewPlayer);
                    CheckAvailablePlayers();
                    checkingPlayers--;
                }
            }
        }
    }


    public  void CheckAvailablePlayers()
    {
        foreach (GameObject GnT in TeamPlayers)
        {
            Debug.Log("Passed here again");
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 1)
            {
                if(!Team1.Contains(GnT))
                { Team1.Add(GnT); }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 2)
            {
                if (!Team2.Contains(GnT))
                { Team2.Add(GnT); }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 3)
            {
                if (!Team3.Contains(GnT))
                { Team3.Add(GnT); }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 4)
            {
                if (!Team4.Contains(GnT))
                { Team4.Add(GnT); }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 5)
            {
                if (!Team5.Contains(GnT))
                { Team5.Add(GnT); }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 6)
            {
                if (!Team6.Contains(GnT))
                { Team6.Add(GnT); }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 7)
            {
                if (!Team7.Contains(GnT))
                { Team7.Add(GnT); }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 8)
            {
                if (!Team8.Contains(GnT))
                { Team8.Add(GnT); }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 9)
            {
                if (!Team9.Contains(GnT))
                { Team9.Add(GnT); }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 10)
            {
                if (!Team10.Contains(GnT))
                { Team10.Add(GnT); }
            }
        }
    }

    public void CheckingList(GameObject isc_Dead)
    {
        Debug.Log("isc_Dead name is " + isc_Dead);
        Debug.Log("isc_Dead team is" + isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber);
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 1)
        {
            Team1.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 2)
        {
            Team2.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 3)
        {
            Team3.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 4)
        {
            Team4.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 5)
        {
            Team5.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 6)
        {
            Team6.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 7)
        {
            Team7.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 8)
        {
            Team8.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 9)
        {
            Team9.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 10)
        {
            Team10.Remove(isc_Dead);
        }
        isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber = 0;
        CheckforTeamStatus();
    }

    private void CheckforTeamStatus()
    {
        Debug.Log("Here I go again on my own");
        if (Team1.Count != 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
            Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 1");
            foreach (GameObject Winners in Team1)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count != 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 2");
            foreach (GameObject Winners in Team2)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count != 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 3");
            foreach (GameObject Winners in Team3)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count != 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 4");
            foreach (GameObject Winners in Team4)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count != 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 5");
            foreach (GameObject Winners in Team5)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count != 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 6");
            foreach (GameObject Winners in Team6)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count != 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 7");
            foreach (GameObject Winners in Team7)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count != 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 8");
            foreach (GameObject Winners in Team8)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count != 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 9");
            foreach (GameObject Winners in Team9)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count != 0)
        {
            Debug.Log("Victory for team 10");
            foreach (GameObject Winners in Team10)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
    }

    public void Init()
    {
        if (WasInit)
            return;

        //Make a score text
        GameObject scoreGO = new GameObject(playerName + "score");
        WasInit = true;
    }
    [ClientRpc]
    public void Rpc_CheckingList(GameObject go) {
        CheckingList(go);
    }
}
