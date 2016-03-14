using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityEngine.Networking;

public class PickUp : NetworkBehaviour {
	RaycastHit hit;
	public float rayDistance;
	public float rayRadius;
	public GameObject ParentFPC;
	public CrabandToss crab;
    [SerializeField]
    private bool m_IsCrouching;
    public bool m_IsRefillingStamina = false;
    public float CrouchSpeed = 3;
    public float NormalSpeed;
    public float RunningSpeed;
    public Transform tr;
    public float OnAndOff = 1;

    // Use this for initialization
    void Start () {
		crab = ParentFPC.GetComponent<CrabandToss> ();
        NormalSpeed = ParentFPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_WalkSpeed;
        RunningSpeed = ParentFPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_RunSpeed;
    }
	
	// Update is called once per frame
	void Update () {
		if (Physics.SphereCast (transform.position, rayRadius,  transform.forward, out hit, rayDistance)) {
			if(hit.collider.GetComponent<DodgeBallScript>() != null){
				print ("Ball!");
				if(Input.GetKeyDown(KeyCode.E) || CrossPlatformInputManager.GetButtonDown("Fire1"))
                {
					crab.GrabBall.SetActive(true);
                    crab.GrabBall.GetComponent<Renderer>().material = hit.collider.gameObject.GetComponent<Renderer>().material;
                    //Changes the balls material to the material the player caught.
                 //   crab.GrabBall.renderer
                    //crab.GrabBall.renderer.material.mainTexture = 
					NetworkServer.Destroy (hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
					crab.GotTheBall = true;

				}
			}
		}
    }

    void FixedUpdate()
    {
        InputtingKeys();
       //A test of Stamina in the game
      /*  if (OnAndOff > 0.02f)
        {
            InputtingKeys();
        }
        else if (OnAndOff < 0.01f)
        {
            ParentFPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_RunSpeed = 5f;
            m_IsRefillingStamina = true;
        }
        if (m_IsRefillingStamina == true)
        {
            Mathf.Lerp(OnAndOff, 1, 2 * Time.deltaTime);
            if(OnAndOff > 9.99f)
            {
                m_IsRefillingStamina = false;
            }
        }*/

    }

    void InputtingKeys()
    {
        if (CrossPlatformInputManager.GetButton("Crouch"))
        { // press C to crouch
          //    vScale = 0.5f;
            ParentFPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_WalkSpeed = 3f;
            ParentFPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_RunSpeed = 6f;
            ParentFPC.transform.localScale = new Vector3(1, Mathf.Lerp(0.79f, 1, 5 * Time.deltaTime),1);
            OnAndOff -= 0.01f;
        }
        else
        {
            ParentFPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_WalkSpeed = NormalSpeed;
            ParentFPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_RunSpeed = RunningSpeed;
            ParentFPC.transform.localScale = new Vector3(1, Mathf.Lerp(1, 0.79f, 5 * Time.deltaTime), 1);
        }
    }

  /*  void DuckLerp()
    {
        switch (OnAndOff)
        {
            case 0:
                text.text = "Add Ships";
                break;
            case 1:
                text.text = "Player Selecting";
                break;
        }
    }*/
}
