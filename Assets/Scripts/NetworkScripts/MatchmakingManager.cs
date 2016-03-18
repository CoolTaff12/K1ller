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
    private bool matchCreated;
    private NetworkMatch networkMatch;
    public GameObject[] PanelNr;

	// Use this for initialization
	void Awake ()
    {
        if(Application.loadedLevel == 0)
        {
            PanelNr = new GameObject[4];
            PanelNr[0] = GameObject.Find("Panel");
            PanelNr[1] = GameObject.Find("Map Panel");
            PanelNr[2] = GameObject.Find("Create Room");
            PanelNr[3] = GameObject.Find("Loading");
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

    //- NetworkManager_Custome

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

    public void FindMatch()
    {
        CreatingRoom();
        hjsgadjasd
        matchMaker.ListMatches(0, 20, "", OnMatchList);
    }

    public void SelectMatch()
    {
     /*   foreach (var match in matches)
        {
            Instantiate(
            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Join Match:" + match.name))
            {
                matchName = match.name;
                matchSize = (uint)match.currentSize;
                matchMaker.JoinMatch(match.networkId, "", manager.OnMatchJoined);
            }
            ypos += spacing;
        }*/
    }

    public void SelectMap()
    {
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
        matchMaker.CreateMatch(matchName, matchSize, true, "", OnMatchCreate);
    }

    void SetRoomName()
    {
        string RoomName = GameObject.Find("InputFieldRoomName").transform.FindChild("Text").GetComponent<Text>().text;
        matchName = RoomName;
        Debug.Log("The amount of matchName is " + matchName);
        Debug.Log("The amount of matchSize is " + matchSize);
    }

    void ChangeToCreate()
    {
        PanelNr[3].SetActive(false);
        PanelNr[2].SetActive(true);
        PanelNr[1].SetActive(false);
        PanelNr[0].SetActive(false);
    }

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
        if(level == 0)
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

}
