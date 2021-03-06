﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapSelection : MonoBehaviour
{
    public UnityStandardAssets.Network.LobbyManager LobbyScenes;
    public Text OptionSelected;
    public Image MapFolder;
    public Sprite[] MapImages;

    // Use this for initialization
    void Start ()
    {
        LobbyScenes = GameObject.Find("LobbyManager").GetComponent< UnityStandardAssets.Network.LobbyManager>();
        OptionSelected = GameObject.Find("OptionSelection").GetComponent<Text>();
        MapFolder = GameObject.Find("MapImage").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(OptionSelected.text == "GYM_FORT")
        {
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "HermanGympasal";
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "GYM_FORT";
            MapFolder.sprite = MapImages[0];
        }
        if (OptionSelected.text == "GYM_ARENA")
        {
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "SergejTestGym";
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "GYM_ARENA";
            MapFolder.sprite = MapImages[1];
        }
        if (OptionSelected.text == "GYM_BRIDGETOWN")
        {
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "BridgeTown";
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "GYM_BRIDGETOWN";
            MapFolder.sprite = MapImages[2];
        }

        if (OptionSelected.text == "GYM_FLOORISLAVA")
        {
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "TheFloorIsLava";
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "GYM_FLOORISLAVA";
            MapFolder.sprite = MapImages[3];
        }

        if (OptionSelected.text == "Bonus_AcrossTheSky")
        {
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "RotateAcrossTheSky";
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "Bonus_AcrossTheSky";
            MapFolder.sprite = MapImages[4];
        }
    }
}
