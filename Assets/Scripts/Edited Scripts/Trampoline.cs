using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour
{
	public Vector3 force = new Vector3(0, 10, 0);
	public bool relative = false;
    [SerializeField]
    private float TrampolineForce;
    public int AppliedForce;
    public int NormalForce;
    public int StrongForce;

    Vector3 _overwrite = Vector3.zero;
	
	void OnTriggerEnter(Collider other)
	{
        //If the object is not the player
		if (other.isTrigger || !other.GetComponent<Rigidbody>())
			return;
		
		_overwrite = -other.GetComponent<Rigidbody>().velocity;
		if (force.x < Mathf.Epsilon)
			_overwrite.x = 0F;
		if (force.y < Mathf.Epsilon)
			_overwrite.y = 0F;
		if (force.z < Mathf.Epsilon)
			_overwrite.z = 0F;
		
		if (relative)
			other.GetComponent<Rigidbody>().AddRelativeForce(force-other.GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
		else
			other.GetComponent<Rigidbody>().AddForce(force-other.GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);

        //If the object is the player
        if (other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>())
        {
            AppliedForce = Random.Range(1, 3);
            if(AppliedForce == 2)
            {
                TrampolineForce = StrongForce;
            }
            else
            {
                TrampolineForce = NormalForce;
            }
            other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_JumpSpeed = TrampolineForce;
            other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_Jump = true;
        }
     }
}
