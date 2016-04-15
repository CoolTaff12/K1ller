using UnityEngine;
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
    public GameObject DodgeballSpawner;
    public bool isinLevel = false;
    public AudioClip[] audioClips;

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        if (gamePlayer.GetComponent<NetworkCharacterInfo>())
        {
            NetworkCharacterInfo characterInfo = gamePlayer.GetComponent<NetworkCharacterInfo>();

            characterInfo.playerName = lobby.name;
            characterInfo.name = characterInfo.playerName;
            characterInfo.checkingTexture = lobby.playersTexture;
            characterInfo.color = lobby.playerColor;
            characterInfo.teamNumber = (lobby.setTeamNumber + 1);
			characterInfo.gameObject.GetComponent<NetworkCharacterInfo>().teamNumber = (lobby.setTeamNumber + 1);
            characterInfo.score = 0;
            characterInfo.lifeCount = 3;
            CheckAvailablePlayers();
            isinLevel = true;
        }
    }

    public void CheckAvailablePlayers()
    {
        GameObject[] TeamPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject GnT in TeamPlayers)
        {
			if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 1)
            {
                Team1.Add(GnT);
            }
			if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 2)
            {
                Team2.Add(GnT);
            }
			if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 3)
            {
                Team3.Add(GnT);
            }
			if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 4)
            {
                Team4.Add(GnT);
            }
			if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 5)
            {
                Team5.Add(GnT);
            }
			if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 6)
            {
                Team6.Add(GnT);
            }
			if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 7)
            {
                Team7.Add(GnT);
            }
			if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 8)
            {
                Team8.Add(GnT);
            }
			if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 9)
            {
                Team9.Add(GnT);
            }
			if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 10)
            {
                Team10.Add(GnT);
            }
        }
    }

    //-----------------Play Audio------------------------
    //This will take the gameobjects AudioSource to switch the audioclips
    public void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClips[clip];
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if(isinLevel)
        {
            StartCoroutine(CheckforVictory(5.0F));
        }
    }

    public void CheackingList(GameObject isc_Dead)
    {
		if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 1)
        {
            foreach (GameObject c_DeadPlayer in Team1)
            {
				if (c_DeadPlayer.GetComponent<PlayerInfo>().c_Dead == true)
                {
                    Team1.Remove(c_DeadPlayer);
                }
            }
        }
		if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 2)
        {
            foreach (GameObject c_DeadPlayer in Team2)
            {
				if (c_DeadPlayer.GetComponent<PlayerInfo>().c_Dead == true)
                {
                    Team2.Remove(c_DeadPlayer);
                }
            }
        }
		if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 3)
        {
            foreach (GameObject c_DeadPlayer in Team3)
            {
				if (c_DeadPlayer.GetComponent<PlayerInfo>().c_Dead == true)
                {
                    Team3.Remove(c_DeadPlayer);
                }
            }
        }
		if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 4)
        {
            foreach (GameObject c_DeadPlayer in Team4)
            {
				if (c_DeadPlayer.GetComponent<PlayerInfo>().c_Dead == true)
                {
                    Team4.Remove(c_DeadPlayer);
                }
            }
        }
		if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 5)
        {
            foreach (GameObject c_DeadPlayer in Team5)
            {
				if (c_DeadPlayer.GetComponent<PlayerInfo>().c_Dead == true)
                {
                    Team5.Remove(c_DeadPlayer);
                }
            }
        }
		if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 6)
        {
            foreach (GameObject c_DeadPlayer in Team6)
            {
				if (c_DeadPlayer.GetComponent<PlayerInfo>().c_Dead == true)
                {
                    Team6.Remove(c_DeadPlayer);
                }
            }
        }
		if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 7)
        {
            foreach (GameObject c_DeadPlayer in Team7)
            {
				if (c_DeadPlayer.GetComponent<PlayerInfo>().c_Dead == true)
                {
                    Team7.Remove(c_DeadPlayer);
                }
            }
        }
		if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 8)
        {
            foreach (GameObject c_DeadPlayer in Team8)
            {
				if (c_DeadPlayer.GetComponent<PlayerInfo>().c_Dead == true)
                {
                    Team8.Remove(c_DeadPlayer);
                }
            }
        }
		if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 9)
        {
            foreach (GameObject c_DeadPlayer in Team9)
            {
				if (c_DeadPlayer.GetComponent<PlayerInfo>().c_Dead == true)
                {
                    Team9.Remove(c_DeadPlayer);
                }
            }
        }
		if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 10)
        {
            foreach (GameObject c_DeadPlayer in Team10)
            {
				if (c_DeadPlayer.GetComponent<PlayerInfo>().c_Dead == true)
                {
                    Team10.Remove(c_DeadPlayer);
                }
            }
        }
    }
    private IEnumerator GoBacktoLobby(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LobbyManager LM = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
        UnityEngine.Cursor.visible = true;
        LM.GoBackButton();
    }

    private IEnumerator CheckforVictory(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (Team1.Count != 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
            Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 1");
            PlaySound(0);
            StartCoroutine(GoBacktoLobby(10F));
        }
        if (Team1.Count == 0 && Team2.Count != 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 2");
            PlaySound(0);
            StartCoroutine(GoBacktoLobby(10F));
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count != 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 3");
            PlaySound(0);
            StartCoroutine(GoBacktoLobby(10F));
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count != 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 4");
            PlaySound(0);
            StartCoroutine(GoBacktoLobby(10F));
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count != 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 5");
            PlaySound(0);
            StartCoroutine(GoBacktoLobby(10F));
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count != 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 6");
            PlaySound(0);
            StartCoroutine(GoBacktoLobby(10F));
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count != 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 7");
            PlaySound(0);
            StartCoroutine(GoBacktoLobby(10F));
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count != 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 8");
            PlaySound(0);
            StartCoroutine(GoBacktoLobby(10F));
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count != 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 9");
            PlaySound(0);
            StartCoroutine(GoBacktoLobby(10F));
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count != 0)
        {
            Debug.Log("Victory for team 10");
            PlaySound(0);
            StartCoroutine(GoBacktoLobby(10F));
        }
    }
}
