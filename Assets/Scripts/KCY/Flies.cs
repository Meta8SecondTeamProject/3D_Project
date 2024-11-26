using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flies : MonoBehaviour
{
	public Transform fliesPos;
	public Transform moveMentPos;
	public float rotateSpeed;
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponentInChildren<Rigidbody>();
	}

	private void Update()
	{
		//fliesPos.RotateAround(moveMentPos.position, Vector3.up, rotateSpeed);
		//rb.position.
	}
}
