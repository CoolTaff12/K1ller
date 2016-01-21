using UnityEngine;
using System.Collections;

public class CrabandToss : MonoBehaviour
{
    public int team;
    public GameObject GrabBall;
    public GameObject TossBall;
    public bool GotTheBall;
    public float speed;
    public GameObject head;

    public GameObject Marker;
    private GameObject Cam;
    Ray ray;
    Camera Aim;
    public AudioClip[] audioClips;

    //-----------------Play Audio------------------------
    //This will take the gameobjects AudioSource to switch the audioclips
    public void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClips[clip];
        GetComponent<AudioSource>().Play();
    }

    // Use this for initialization
    void Start ()
    {
       Marker = GameObject.Find("Shoot it,");
       Cam = GameObject.Find("FirstPersonCharacter");
     //  Aim = Cam;
       GrabBall.SetActive(false);
        if(team == 1)
        {

        }
        else
        {

        }
    }
	
	// Update is called once per frame
	void Update ()
    {
      //  ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (GotTheBall == true)
        {
            ShootBall();
        }
    }

    void ShootBall()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            GrabBall.SetActive(false);
            /*Vector3 Distance = new Vector3(transform.position.x, transform.position.y + 0.8f,
                                                                 transform.position.z + 1.4f);
            Quaternion Direction = new Quaternion(transform.rotation.x, transform.rotation.y - 180f,
                                                                    transform.rotation.z);*/
            GameObject SpeedingBall = Instantiate(TossBall, Marker.transform.position, Marker.transform.rotation) as GameObject;
            Rigidbody rb = SpeedingBall.GetComponent<Rigidbody>();
            rb.AddForce(head.transform.forward * speed);
         //   instantiatedBall.AddForce(instantiatedBall.transform.forward * speed);

            GotTheBall = false;
        }
    }

}
