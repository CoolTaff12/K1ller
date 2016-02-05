using UnityEngine;
using System.Collections;

public class TrampScript : MonoBehaviour
{
    public float manualforce;
    public PlayerTarget playerInfo;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerInfo = col.gameObject.GetComponent<PlayerTarget>();
            Vector3 velocity = col.gameObject.GetComponent<Rigidbody>().velocity;
            col.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, manualforce, velocity.z);
         //   rb.AddForce(col.gameObject.transform.up * manualforce);         
        }
    }
}
