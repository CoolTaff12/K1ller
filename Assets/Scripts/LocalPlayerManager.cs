using UnityEngine;
using System.Collections;

public class LocalPlayerManager : GameManager
{
    GameObject NetworkM;
    public GameObject[] Players;

    // Use this for initialization
    void Start ()
    {
        if (NetworkM = GameObject.Find("NetworkManager"))
        {
            if (!GameManager.IsUsingNework)
            {
                Destroy(NetworkM);
            }
            else
                Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
