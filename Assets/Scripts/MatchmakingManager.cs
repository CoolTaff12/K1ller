using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class MatchmakingManager : NetworkManager
{

    private NetworkMatch networkMatch;

	// Use this for initialization
	void Start () {
	
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

    void SetIPAddress()
    {
        string ipAdress = GameObject.Find("InputFieldIPAddress").transform.FindChild("Test").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAdress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

}
