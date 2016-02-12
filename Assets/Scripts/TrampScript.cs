using UnityEngine;
using System.Collections;

public class TrampScript : MonoBehaviour
{

    public float manualforce;
    public PlayerTarget playerInfo;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController warriorInfo;
    public Rigidbody rb;
    private float startTime;

    // Use this for initialization
    void Start ()
    {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            float NewPosition = col.gameObject.transform.position.y + manualforce;
            Vector3 Before = col.gameObject.transform.position;
            Vector3 After = new Vector3 (col.gameObject.transform.position.x, NewPosition, gameObject.transform.position.z);
            float fracComplete = (Time.time - startTime) / 1.0f;
            col.gameObject.transform.position = Vector3.Lerp(Before, After, fracComplete);





              /*  = new Vector3(col.gameObject.transform.position.x,
                Mathf.Lerp(col.gameObject.transform.position.y, NewPosition, 50 * Time.deltaTime),
                col.gameObject.transform.position.z);
            manualforce--;*/
            //playerInfo = col.gameObject.GetComponent<PlayerTarget>();
           // warriorInfo = col.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        /*    Vector3 velocity = col.gameObject.GetComponent<Rigidbody>().velocity;
			rb = col.gameObject.GetComponent<Rigidbody> ();
			rb.velocity = new Vector3(rb.velocity.x, manualforce, rb.velocity.z);*/
         //   rb.AddForce(col.gameObject.transform.up * manualforce);         
        }
    }
}
