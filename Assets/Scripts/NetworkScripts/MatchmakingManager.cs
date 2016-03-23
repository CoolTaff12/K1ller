using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System.Collections;
using System.Collections.Generic;

public class MatchmakingManager : NetworkManager
{

    [SerializeField]
    private List<MatchDesc> matchList = new List<MatchDesc>();
    private MatchDesc currentMatch;
    private bool matchCreated;
    private NetworkMatch networkMatch;
    public GameObject[] PanelNr;
    GameObject JoinMatchButton;

	// Use this for initialization
	void Awake ()
    {
        networkMatch = gameObject.AddComponent<NetworkMatch>();
        if (Application.loadedLevel == 0)
        {
            PanelNr = new GameObject[5];
            PanelNr[0] = GameObject.Find("Panel");
            PanelNr[1] = GameObject.Find("Map Panel");
            PanelNr[2] = GameObject.Find("Create Room");
            PanelNr[3] = GameObject.Find("Loading");
            PanelNr[4] = GameObject.Find("List of Matches");
            JoinMatchButton = Resources.Load("Prefabs/Menu/Join Match Button") as GameObject;
            PanelNr[4].SetActive(false);
            PanelNr[3].SetActive(false);
            PanelNr[2].SetActive(false);
            PanelNr[1].SetActive(false);
            PanelNr[0].SetActive(true);
        }
	}

    // Update is called once per frame
    void Update ()
    {
	    if(networkMatch == null)
        {
            NetworkMatch nm = GetComponent<NetworkMatch>();   
            if( nm != null)
            {
                networkMatch = nm;
                //  AppID appid;
                //appid = 837002;
                networkMatch.SetProgramAppID((AppID) 837002);
            }
        }
	}

    //------------------------------------- NetworkManager_Custome

    public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGameNow()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }


    void SetIPAddress()
    {
        string ipAdress = GameObject.Find("InputFieldIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAdress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            SetUpMenuSceneButton();
            StartCoroutine(SetUpMenuSceneButton());
        }
        else
        {
            SetUpOtherMenuSceneButton();
        }
    }

    IEnumerator SetUpMenuSceneButton()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("" + onlineScene).GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("" + onlineScene).GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("Find Match").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Find Match").GetComponent<Button>().onClick.AddListener(JoinGameNow);
    }

    void SetUpOtherMenuSceneButton()
    {
        /*  GameObject.Find("Disconect").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("Disconect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);*/
    }

    //----------------------------Hosting Matches

    public void SelectMap()
    {
        PanelNr[4].SetActive(false);
        PanelNr[3].SetActive(false);
        PanelNr[2].SetActive(false);
        PanelNr[1].SetActive(true);
        PanelNr[0].SetActive(false);
        StartMatchMaker();
    }

    public void CreatingRoom()
    {
        StartMatchMaker();
    }

    public void CreateInternetMatch()
    {
       SetRoomName();
       PanelNr[3].SetActive(true);
        StartCoroutine("LoadingARoom", 5.0f);
    }

    void LoadingARoom()
    {
        CreateMatchRequest create = new CreateMatchRequest();
        create.name = matchName;
        create.size = matchSize;
        create.advertise = true;
        create.password = "";
        networkMatch.CreateMatch(create, OnMatchCreate);
    }

    void SetRoomName()
    {
        string RoomName = GameObject.Find("InputFieldRoomName").transform.FindChild("Text").GetComponent<Text>().text;
        matchName = RoomName;
    }

    void ChangeToCreate()
    {
        PanelNr[4].SetActive(false);
        PanelNr[3].SetActive(false);
        PanelNr[2].SetActive(true);
        PanelNr[1].SetActive(false);
        PanelNr[0].SetActive(false);
    }

    //------------------------------------- Finding matches on matchlist

    public void FindMatch()
    {
        CreatingRoom();
        PanelNr[4].SetActive(true);
        PanelNr[0].SetActive(false);
        networkMatch.ListMatches(0, 20, "", OnMatchList);
    }

    //--------------Copy and Paste

    public void OnMatchList(ListMatchResponse matchListResponse)
    {
        if (matchListResponse.success && matchListResponse.matches != null)
        {
            foreach (var match in matchListResponse.matches)
            {
                GameObject MatchChooiceNr = (GameObject)Instantiate(JoinMatchButton, Vector3.zero,
                Quaternion.identity);
                MatchChooiceNr.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                MatchChooiceNr.transform.parent = PanelNr[4].transform;
                MatchChooiceNr.name = "Join Match: " + match.name;
                MatchChooiceNr.transform.FindChild("Text").GetComponent<Text>().text = "Join Match: " + "\n" + match.name + "\n";
                MatchChooiceNr.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("NetworkManager").GetComponent<MatchmakingManager>().MatchName(match));

            }
        }
    }

    public void MatchName(MatchDesc avaliblematch)
    {
        Debug.Log("Over Here!");
        currentMatch = avaliblematch;
        Debug.Log("Match name is "+ currentMatch.name);
        networkMatch.JoinMatch(currentMatch.networkId, "", OnMatchJoined);
    }

    public void OnMatchJoined(JoinMatchResponse matchJoin)
    {
        if (matchJoin.success)
        {
            Debug.Log("Join match succeeded");
            if (matchCreated)
            {
                Debug.LogWarning("Match already set up, aborting...");
                return;
            }
            Utility.SetAccessTokenForNetwork(matchJoin.networkId, new NetworkAccessToken(matchJoin.accessTokenString));
            NetworkClient myClient = new NetworkClient();
            myClient.RegisterHandler(MsgType.Connect, OnConnected);
            myClient.Connect(new MatchInfo(matchJoin));
        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }

    public void OnConnected(NetworkMessage msg)
    {
        Debug.Log("Connected!");
    }

    //------------------------ Map selection

    public void Gym()
    {
        onlineScene = "HermanGympasal";
        ChangeToCreate();
    }

    public void Arena()
    {
        onlineScene = "HermanArenaTest";
        ChangeToCreate();
    }

    public void Chinese()
    {
        onlineScene = "Chinese";
        ChangeToCreate();
        // StartupHost();
    }

}