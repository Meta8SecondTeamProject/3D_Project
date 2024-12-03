using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_MeshMovement : MonoBehaviour
{
	private Rigidbody rb;
	private Rigidbody parent_Rb;
	public Transform lookPos;
	private Vector3 rotDir;
	private void Awake()
	{
		parent_Rb = GetComponentInParent<Rigidbody>();
		rb = GetComponent<Rigidbody>();

	}

	private void FixedUpdate()
	{
		rb.MovePosition(parent_Rb.position);
		Look();
	}

	private void Look()
	{
		rotDir = lookPos.position - Vector3.left;
		Quaternion newRotation = Quaternion.LookRotation(rotDir);
		rb.MoveRotation(newRotation);
	}

}
