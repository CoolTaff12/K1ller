using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System;
using System.Collections;
using System.Collections.Generic;

public class MatchMakingNet : MonoBehaviour
{

    private MatchDesc currentMatch;
    private bool matchCreated;
    private NetworkMatch networkMatch;
    private NetworkManager NM;

    public GameObject[] PanelNr;
    GameObject JoinMatchButton;

    // Use this for initialization
    void Start ()
    {
        NM = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        PanelNr = new GameObject[5];
        PanelNr[0] = GameObject.Find("Panel");
        PanelNr[1] = GameObject.Find("Map Panel");
        PanelNr[2] = GameObject.Find("Create Room");
        PanelNr[3] = GameObject.Find("Loading Canvas");
        PanelNr[4] = GameObject.Find("List of Matches");
        JoinMatchButton = Resources.Load("Prefabs/Menu/Join Match Button") as GameObject;
        PanelNr[4].SetActive(false);
        PanelNr[3].SetActive(false);
        PanelNr[2].SetActive(false);
        PanelNr[1].SetActive(false);
        PanelNr[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkClient.active && !ClientScene.ready)
        {
            Debug.Log("Are we there yet?");
            ClientScene.Ready(NM.client.connection);

            if (ClientScene.localPlayers.Count == 0)
            {
                Debug.Log("Yes we are");
                ClientScene.AddPlayer(0);
            }
        }
    }

    //----------------------------Hosting Matches

    public void SelectMap() //--Function that attached to Host Match
    {
        if (NM.matchMaker == null)
        {
            PanelNr[4].SetActive(false);
            PanelNr[3].SetActive(false);
            PanelNr[2].SetActive(false);
            PanelNr[1].SetActive(true);
            PanelNr[0].SetActive(false);
            NM.StartMatchMaker();
        }
    }

    //------------------------ Map selection

    public void Gym_1()
    {
        NM.onlineScene = "HermanGympasal";
        ChangeToCreate();
    }

    public void Gym_2()
    {
        NM.onlineScene = "SergejTestGym";
        ChangeToCreate();
    }

    public void Arena()
    {
        NM.onlineScene = "HermanArenaTest";
        ChangeToCreate();
    }

    public void Chinese()
    {
        NM.onlineScene = "Chinese";
        ChangeToCreate();
    }

    // Adding Name and Hosting the Match

    void ChangeToCreate()
    {
        PanelNr[4].SetActive(false);
        PanelNr[3].SetActive(false);
        PanelNr[2].SetActive(true);
        PanelNr[1].SetActive(false);
        PanelNr[0].SetActive(false);
    }

    public void CreateInternetMatch() //---- Hosting it's name
    {
        SetRoomName();
        PanelNr[3].SetActive(true);
        PanelNr[0] = null;
        PanelNr[1] = null;
        PanelNr[2] = null;
        PanelNr[4] = null;
        CheckingTheClient();
        NM.matchMaker.CreateMatch(NM.matchName, NM.matchSize, true, "", NM.OnMatchCreate);
    }

    void SetRoomName() // Makes sure that The Rooms Name is what is in the InputField
    {
        string RoomName = GameObject.Find("InputFieldRoomName").transform.FindChild("Text").GetComponent<Text>().text;
        NM.matchName = RoomName;
    }

    //------------------------------------- Finding matches on matchlist

    public void FindMatch() //--Function that is attached to the FInd Match Button
    {
        CreatingRoom();
        PanelNr[4].SetActive(true);
        PanelNr[0].SetActive(false);
        NM.matchMaker.ListMatches(0, 20, "", NM.OnMatchList);
        MatchesFound();
    }

    void CreatingRoom()
    {
        NM.StartMatchMaker();
    }

    public void MatchesFound()
    {
        foreach (var match in NM.matches)
        {
            GameObject MatchChooiceNr = (GameObject)Instantiate(JoinMatchButton, Vector3.zero,
            Quaternion.identity);
            MatchChooiceNr.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            MatchChooiceNr.transform.parent = PanelNr[4].transform;
            MatchChooiceNr.name = "Join Match: " + match.name;
            MatchChooiceNr.transform.FindChild("Text").GetComponent<Text>().text = "Join Match: " + "\n" + match.name + "\n";
            MatchChooiceNr.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("GUI Manager").GetComponent<MatchmakingManager>().MatchName(match));

        }
    }

    //---------------------- Instantiate Client ready.

    void CheckingTheClient()
    {
        if (NetworkClient.active && !ClientScene.ready)
        {
            Debug.Log("Client is ready");
            GameObject ConfirmationButton = (GameObject)Instantiate(JoinMatchButton, Vector3.zero, Quaternion.identity);
            ConfirmationButton.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            ConfirmationButton.transform.parent = PanelNr[3].transform;
            ConfirmationButton.name = "Are you ready?";
            ConfirmationButton.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("GUI Manager").GetComponent<MatchMakingNet>().isTheClientReady());
        }
    }

    public void isTheClientReady()
    {
        Debug.Log("Are we there yet?");
        ClientScene.Ready(NM.client.connection);

        if (ClientScene.localPlayers.Count == 0)
        {
            ClientScene.AddPlayer(1);
        }
    }

}
