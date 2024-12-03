using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_MeshMovement : MonoBehaviour
{
	private Rigidbody rb;
	private Rigidbody parent_Rb;
	public Transform lookPos;
	public Transform lookDir;
	private Vector3 rotDir;
	private void Awake()
	{
		parent_Rb = GetComponentInParent<Rigidbody>();
		rb = GetComponent<Rigidbody>();

	}

	private void FixedUpdate()
	{
		Look();
	}

	private void Look()
	{
		//lookDir.LookAt(lookPos);
	}

}
