using UnityEngine;
using UnityStandardAssets.Network;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook
{
    public bool isinLevel = false;
    public bool showResults = false;
    public GameObject DodgeballSpawner;
    public GameObject OurPlayer;

    public LobbyManager LM;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController MouseLocking;

    /// <param name="manager">The NetworkManager</param>
    /// <param name="lobbyPlayer">the lobbyPlayer that gives out values to NetworkCharacterInfo</param>
    /// <param name="gameplayer">Makes Ourplayer the same as the selected gamePlayer</param>
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        if (gamePlayer.GetComponent<NetworkCharacterInfo>())
        {
            OurPlayer = gamePlayer;
            MouseLocking = OurPlayer.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
            NetworkCharacterInfo characterInfo = gamePlayer.GetComponent<NetworkCharacterInfo>();
            

            characterInfo.playerName = lobby.name;
            characterInfo.name = characterInfo.playerName;
            characterInfo.checkingTexture = lobby.playersTexture;
            characterInfo.color = lobby.playerColor;
            characterInfo.teamNumber = (lobby.setTeamNumber + 1);
            characterInfo.gameObject.GetComponent<NetworkCharacterInfo>().teamNumber = (lobby.setTeamNumber + 1);
            isinLevel = true;
        }
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && showResults == false)
        {
            if(OurPlayer != null)
            {
                LM.Exiting(MouseLocking, true);
            }
        }
        else if(Input.GetKey(KeyCode.Escape) && showResults == false && isinLevel == false)
        {
            LM.GoBackButton();
        }
    }


    public IEnumerator GoBacktoLobby(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isinLevel = false;
        showResults = false;
        MouseLocking.m_MouseLook.lockCursor = false;
        UnityEngine.Cursor.visible = true;
        LM.Exiting(MouseLocking, true);
    }

    
}
