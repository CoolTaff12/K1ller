using UnityEngine;
using System.Collections;

public class DodgeBallScript : MonoBehaviour
{
    public AudioClip[] audioClips = new AudioClip[1];
	public PlayerTarget playerInfo;
	public int thrownByTeam = 1;
    public GameObject Sparks;

    // Use this for initialization
    void Start ()
    {
        audioClips[0] = Resources.Load("Basketball-BallBounce") as AudioClip;
        Sparks = Resources.Load("New Import/Particles/child prefabs/enmy Death") as GameObject;
	}
	
	// Update is called once per frame;
	void Update () {
	
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
            PlaySound(0);
        }

    }
}
