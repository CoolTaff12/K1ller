﻿using UnityEngine;
using UnityStandardAssets.Network;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        if(gamePlayer.GetComponent<NetworkSpaceship>())
        {
            NetworkSpaceship spaceship = gamePlayer.GetComponent<NetworkSpaceship>();

            spaceship.name = lobby.name;
            spaceship.color = lobby.playerColor;
            spaceship.score = 0;
            spaceship.lifeCount = 3;
        }
        else if(gamePlayer.GetComponent<NetworkCharacterInfo>())
        {
            NetworkCharacterInfo characterInfo = gamePlayer.GetComponent<NetworkCharacterInfo>();

            characterInfo.name = lobby.name;
            characterInfo.color = lobby.playerColor;
            characterInfo.score = 0;
            characterInfo.lifeCount = 3;
        }
    }
}