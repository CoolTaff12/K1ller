/*
 * Copyright (C) 2015 Bun Bun Studios.
 *
 * This software is distributed to you as-is without any guarantees or warranties of any kind. You agree 
 * to use this software at your own risk. Bun Bun Studios is not liable for anything directly or indirectly. You 
 * are not allowed to copy, distribute, redistribute, sell, resell, reproduce or transfer this software or 
 * derivations of this software.
 * 
 */

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
        Debug.Log("Hey, what are you doing here?!");
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
            Debug.Log("Gottcha!");
        }
     }
}
