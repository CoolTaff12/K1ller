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
        //Finds all spawn points on the map
        respawns = GameObject.FindGameObjectsWithTag("Spawn");

        foreach (GameObject spawns in respawns)
        {
            SpawningPositions.Add(spawns);
        }
    }
	
    //If object has fallen out of bound, they get send to one of the spawn points on the map
    void OnTriggerStay(Collider target)
    {
        int Selected = Random.Range(0, SpawningPositions.Count);
        target.transform.position = SpawningPositions[Selected].transform.position;
    }
}
