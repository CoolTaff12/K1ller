using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DodgeBallScript : NetworkBehaviour
{
    public AudioClip[] audioClips;
	public PlayerTarget playerInfo;
	[SyncVar]
	public int thrownByTeam = 1;
    public GameObject Sparks;
    [SerializeField]
    private Transform myTransform;

    // Use this for initialization
    void Start ()
    {
        if(isLocalPlayer)
        {
            Sparks = Resources.Load("Particles/child prefabs/enmy Death") as GameObject;
        }
        else
        {
            return;
        }
	}
	
	// Update is called once per frame;
	void Update ()
    {
	 
	}
    //-----------------Play Audio------------------------
    //This will take the gameobjects AudioSource to switch the audioclips
    public void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClips[clip];
        GetComponent<AudioSource>().Play();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
			playerInfo = col.gameObject.GetComponent<PlayerTarget>();
			if (playerInfo.teamNumber != thrownByTeam) {
				playerInfo.health -= 1;
			}

        }
        if (col.gameObject.tag == "ForceField")
        {
           GameObject Sparked = (GameObject) Instantiate(Sparks, transform.position, Quaternion.identity);
            Destroy(Sparked, 3f);
        }
        else if (col.gameObject.tag != "Player" || col.gameObject.tag != "ForceField")
        {
            int SelectSoundFile = Random.Range(0, 4);
            PlaySound(SelectSoundFile);
        }

    }
}
