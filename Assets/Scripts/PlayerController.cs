using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField] private bool IsWalking;
    [SerializeField] private bool IsRunning;
    private Vector3 MoveDir = Vector3.zero;
    private bool PreviouslyGrounded;
    private CharacterController CController;
    private Camera PlayersCamera;
    private Vector3 OriginalCameraPosition;

    public string axisX = "Horizontal";
    public string axisY = "Vertical";
    public float speed;
    public float WalkSpeed;
    public float RunSpeed;

    // Use this for initialization
    void Start ()
    {
        GameObject PreviewCamera = GameObject.Find("Preview Camera");
        Destroy(PreviewCamera);
        CController = GetComponent<CharacterController>();
        PlayersCamera = gameObject.GetComponentInChildren<Camera>();
        OriginalCameraPosition = PlayersCamera.transform.localPosition;
        IsWalking = true;
        IsRunning = false;
        speed = WalkSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isLocalPlayer)
        {
            GettingInput();
            transform.position += Input.GetAxis(axisX) * Vector3.right * speed * Time.deltaTime;
            transform.position += Input.GetAxis(axisY) * Vector3.forward * speed * Time.deltaTime;
            PreviouslyGrounded = CController.isGrounded;
        }
    }

    void GettingInput()
    {
            if (IsWalking == true)
            {
                speed = WalkSpeed;
            }
            else if (IsRunning == true)
            {
                speed = RunSpeed;
            }
            if (Input.GetButton("Sprint"))
            {
                IsRunning = true;
                IsWalking = false;
            }
            else
            {
                IsRunning = false;
                IsWalking = true;
            }
    }
}
