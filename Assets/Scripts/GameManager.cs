using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static bool IsUsingNework;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void NetworkMenu()
    {
        IsUsingNework = true;
    }

    public void LocalMenu()
    {
        IsUsingNework = false;
    }
}
