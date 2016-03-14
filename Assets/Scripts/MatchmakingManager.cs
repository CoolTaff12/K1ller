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
	void Start ()
    {
        if(Application.loadedLevel == 0)
        {
            PanelNr = new GameObject[4];
            PanelNr[0] = GameObject.Find("Panel");
            PanelNr[1] = GameObject.Find("Map Panel");
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

    public void SelectMap()
    {
        PanelNr[1].SetActive(true);
        PanelNr[0].SetActive(false);
    }

    public void Gym()
    {
        onlineScene = "HermanGympasal";
        StartupHost();
    }

    public void Arena()
    {
        onlineScene = "HermanArenaTest";
        StartupHost();
    }

    public void Chinese()
    {
        onlineScene = "Chinese";
        StartupHost();
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
        GameObject.Find("Host Match").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Host Match").GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("Find Match").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Find Match").GetComponent<Button>().onClick.AddListener(JoinGameNow);
    }

    void SetUpOtherMenuSceneButton()
    {
    /*  GameObject.Find("Disconect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Disconect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);*/
    }

}
