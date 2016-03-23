using UnityEngine;
using System.Collections;

public class testThrowBall : MonoBehaviour 
{
	public GameObject ballPrefab;
	public float ballSpeed = 1f;
	public Transform spawnPoint;
	public Rigidbody rb;
	public float killTime = 3f;


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{


	}

	public void ThrowBall()
	{
//		Instantiate (ballPrefab, spawnPoint.transform.position, Quaternion.identity);
		GameObject ballClone = Instantiate(ballPrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;

		rb = ballClone.GetComponent<Rigidbody>();
		rb.AddForce(transform.forward * ballSpeed);

		rb.velocity = transform.TransformDirection(new Vector3(0, 0, ballSpeed));
		Destroy(ballClone, killTime);
	}
}
