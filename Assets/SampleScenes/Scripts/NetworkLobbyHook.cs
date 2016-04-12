﻿using UnityEngine;
using UnityStandardAssets.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook
{
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

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        if (gamePlayer.GetComponent<NetworkSpaceship>())
        {
            NetworkSpaceship spaceship = gamePlayer.GetComponent<NetworkSpaceship>();

            spaceship.name = lobby.name;
            spaceship.color = lobby.playerColor;
            spaceship.score = 0;
            spaceship.lifeCount = 3;
        }
        else if (gamePlayer.GetComponent<NetworkCharacterInfo>())
        {
            NetworkCharacterInfo characterInfo = gamePlayer.GetComponent<NetworkCharacterInfo>();

            characterInfo.playerName = lobby.name;
            characterInfo.name = characterInfo.playerName;
            characterInfo.color = lobby.playerColor;
            characterInfo.teamNumber = (lobby.setTeamNumber + 1);
            characterInfo.gameObject.GetComponent<GrabAndToss>().teamNumber = (lobby.setTeamNumber + 1);
            characterInfo.score = 0;
            characterInfo.lifeCount = 3;
            CheckAvailablePlayers();
        }
    }

    public void CheckAvailablePlayers()
    {
        GameObject[] TeamPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject GnT in TeamPlayers)
        {
            if (GnT.GetComponent<GrabAndToss>().teamNumber == 1)
            {
                Team1.Add(GnT);
            }
            if (GnT.GetComponent<GrabAndToss>().teamNumber == 2)
            {
                Team2.Add(GnT);
            }
            if (GnT.GetComponent<GrabAndToss>().teamNumber == 3)
            {
                Team3.Add(GnT);
            }
            if (GnT.GetComponent<GrabAndToss>().teamNumber == 4)
            {
                Team4.Add(GnT);
            }
            if (GnT.GetComponent<GrabAndToss>().teamNumber == 5)
            {
                Team5.Add(GnT);
            }
            if (GnT.GetComponent<GrabAndToss>().teamNumber == 6)
            {
                Team6.Add(GnT);
            }
            if (GnT.GetComponent<GrabAndToss>().teamNumber == 7)
            {
                Team7.Add(GnT);
            }
            if (GnT.GetComponent<GrabAndToss>().teamNumber == 8)
            {
                Team8.Add(GnT);
            }
            if (GnT.GetComponent<GrabAndToss>().teamNumber == 9)
            {
                Team9.Add(GnT);
            }
            if (GnT.GetComponent<GrabAndToss>().teamNumber == 10)
            {
                Team10.Add(GnT);
            }
        }
        return;
    }

    void Update()
    {

    }

    public void CheackingList()
    {
         foreach(GameObject deadPlayer in Team1)
         {
             if(deadPlayer.GetComponent<GrabAndToss>().dead == true)
             {
                 Team1.Remove(deadPlayer);
             }
         }
        foreach (GameObject deadPlayer in Team2)
        {
            if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
            {
                Team2.Remove(deadPlayer);
            }
        }
        foreach (GameObject deadPlayer in Team3)
        {
            if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
            {
                Team3.Remove(deadPlayer);
            }
        }
        foreach (GameObject deadPlayer in Team4)
        {
            if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
            {
                Team4.Remove(deadPlayer);
            }
        }
        foreach (GameObject deadPlayer in Team5)
        {
            if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
            {
                Team5.Remove(deadPlayer);
            }
        }
        foreach (GameObject deadPlayer in Team6)
        {
            if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
            {
                Team6.Remove(deadPlayer);
            }
        }
        foreach (GameObject deadPlayer in Team7)
        {
            if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
            {
                Team7.Remove(deadPlayer);
            }
        }
        foreach (GameObject deadPlayer in Team8)
        {
            if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
            {
                Team8.Remove(deadPlayer);
            }
        }
        foreach (GameObject deadPlayer in Team9)
        {
            if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
            {
                Team9.Remove(deadPlayer);
            }
        }
        foreach (GameObject deadPlayer in Team10)
        {
            if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
            {
                Team10.Remove(deadPlayer);
            }
        }
    }
}
