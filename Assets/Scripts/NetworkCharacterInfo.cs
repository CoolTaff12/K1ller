using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;



[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class NetworkCharacterInfo : NetworkBehaviour
{

    //Network syncvar
    [SyncVar]
    public Color color;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public int teamNumber;

    [SyncVar]
    public Texture playertexture;
    public Texture[] selectableTextures;
    [SyncVar]
    public int checkingTexture;


    //hard to control WHEN Init is called (networking make order between object spawning non deterministic)
    //so we call init from multiple location (depending on what between spaceship & manager is created first).
    protected bool WasInit = false;

    // Use this for initialization
    void Start ()
    {
        //Tells the player what its name is
        name = playerName;
        //Renderar the colour and the texture player had choosen ealier
        Renderer[] CRends = GetComponentsInChildren<Renderer>();
        if(checkingTexture == 1)
        {
            playertexture = selectableTextures[0];
        }
        if (checkingTexture == 2)
        {
            playertexture = selectableTextures[1];
        }
        if (checkingTexture == 3)
        {
            playertexture = selectableTextures[2];
        }
        Color headcolor = GameObject.Find(this.gameObject.name + "/Body/regular_dude_body").GetComponent<Renderer>().material.color;
        foreach (Renderer r in CRends)
        {
            r.material.mainTexture = playertexture;
            r.material.color = color;
        }
        GameObject.Find(this.gameObject.name + "/Body/regular_dude_head").GetComponent<Renderer>().material.color = headcolor;
        //Two Materials
   /*     GameObject.Find(this.gameObject.name + "/Body/regular_dude_right_hand").GetComponent<Renderer>().material.color = headcolor;
        GameObject.Find(this.gameObject.name + "/Body/regular_dude_right_hand").GetComponent<Renderer>().material.color = headcolor;*/
    }
	
	// Update is called once per frame
	void Update () {
        UnityEngine.Cursor.visible = true;
    }

    public void Init()
    {
        if (WasInit)
            return;

        //Make a score text
        GameObject scoreGO = new GameObject(playerName + "score");
        WasInit = true;
    }
}
