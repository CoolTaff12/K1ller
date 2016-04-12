using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutofBound : MonoBehaviour
{

    public List<GameObject> SpawningPositions;
    public GameObject[] respawns;

    // Use this for initialization
    void Start ()
    {
        respawns = GameObject.FindGameObjectsWithTag("Spawn");

        foreach (GameObject spawns in respawns)
        {
            SpawningPositions.Add(spawns);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    //If object has fallen out of bound
    void OnTriggerStay(Collider target)
    {
        int Selected = Random.Range(0, SpawningPositions.Count);
        target.transform.position = SpawningPositions[Selected].transform.position;
    }
}
