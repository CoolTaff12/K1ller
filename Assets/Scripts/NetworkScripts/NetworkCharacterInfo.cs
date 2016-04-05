using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;



[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class NetworkCharacterInfo : NetworkBehaviour
{

    //Network syncvar
    [SyncVar(hook = "OnScoreChanged")]
    public int score;
    [SyncVar]
    public Color color;
    [SyncVar]
    public string playerName;
    [SyncVar(hook = "OnLifeChanged")]
    public int lifeCount;
    [SyncVar]
    public int teamNumber = 1;

    // Use this for initialization
    void Start ()
    {

        Renderer[] CRends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in CRends)
        {
            r.material.color = color;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    [ClientRpc]
    public void Rpc_SetTeamNumber(GameObject go)
    {
        go.GetComponent<GrabAndToss>().teamNumber = teamNumber;
        teamNumber++;
    }
}
