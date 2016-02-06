using UnityEngine;
using System.Collections;

public class TrampScript : MonoBehaviour
{
    public float manualforce;
    public PlayerTarget playerInfo;
	public Rigidbody rb;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerInfo = col.gameObject.GetComponent<PlayerTarget>();
            Vector3 velocity = col.gameObject.GetComponent<Rigidbody>().velocity;
			rb = col.gameObject.GetComponent<Rigidbody> ();
			rb.velocity = new Vector3(rb.velocity.x, manualforce, rb.velocity.z);
         //   rb.AddForce(col.gameObject.transform.up * manualforce);         
        }
    }
}
