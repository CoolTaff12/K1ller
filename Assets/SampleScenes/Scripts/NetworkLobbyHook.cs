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
            isinLevel = true;
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
            /*DodgeballSpawner = GameObject.Find("Ballspawner");
            GameObject[] Dodgeballs = GameObject.FindGameObjectsWithTag("Ball");
            if (Dodgeballs[2] != null)
            {
                DodgeballSpawner.SetActive(false);
            }
            else
            {
                DodgeballSpawner.SetActive(true);
            }*/
            StartCoroutine(CheckforVictory(5.0F));
        }
    }

    public void CheackingList(GameObject isdead)
    {
        if (isdead.GetComponent<GrabAndToss>().teamNumber == 1)
        {
            foreach (GameObject deadPlayer in Team1)
            {
                if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
                {
                    Team1.Remove(deadPlayer);
                }
            }
        }
        if (isdead.GetComponent<GrabAndToss>().teamNumber == 2)
        {
            foreach (GameObject deadPlayer in Team2)
            {
                if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
                {
                    Team2.Remove(deadPlayer);
                }
            }
        }
        if (isdead.GetComponent<GrabAndToss>().teamNumber == 3)
        {
            foreach (GameObject deadPlayer in Team3)
            {
                if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
                {
                    Team3.Remove(deadPlayer);
                }
            }
        }
        if (isdead.GetComponent<GrabAndToss>().teamNumber == 4)
        {
            foreach (GameObject deadPlayer in Team4)
            {
                if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
                {
                    Team4.Remove(deadPlayer);
                }
            }
        }
        if (isdead.GetComponent<GrabAndToss>().teamNumber == 5)
        {
            foreach (GameObject deadPlayer in Team5)
            {
                if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
                {
                    Team5.Remove(deadPlayer);
                }
            }
        }
        if (isdead.GetComponent<GrabAndToss>().teamNumber == 6)
        {
            foreach (GameObject deadPlayer in Team6)
            {
                if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
                {
                    Team6.Remove(deadPlayer);
                }
            }
        }
        if (isdead.GetComponent<GrabAndToss>().teamNumber == 7)
        {
            foreach (GameObject deadPlayer in Team7)
            {
                if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
                {
                    Team7.Remove(deadPlayer);
                }
            }
        }
        if (isdead.GetComponent<GrabAndToss>().teamNumber == 8)
        {
            foreach (GameObject deadPlayer in Team8)
            {
                if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
                {
                    Team8.Remove(deadPlayer);
                }
            }
        }
        if (isdead.GetComponent<GrabAndToss>().teamNumber == 9)
        {
            foreach (GameObject deadPlayer in Team9)
            {
                if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
                {
                    Team9.Remove(deadPlayer);
                }
            }
        }
        if (isdead.GetComponent<GrabAndToss>().teamNumber == 10)
        {
            foreach (GameObject deadPlayer in Team10)
            {
                if (deadPlayer.GetComponent<GrabAndToss>().dead == true)
                {
                    Team10.Remove(deadPlayer);
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
