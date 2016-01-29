using UnityEngine;
using System.Collections;

public class DodgeBallScript : MonoBehaviour
{
    public AudioClip[] audioClips;
	public PlayerTarget playerInfo;
	public int thrownByTeam = 1;

    // Use this for initialization
    void Start () {
	
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
		else if (col.gameObject.tag != "Player")
		{
			PlaySound(0);
		}

    }
}
