using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RotateTheRings : MonoBehaviour
{
    public float RotateSpeed;

	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0f, RotateSpeed, 0.0f));
    }
}
