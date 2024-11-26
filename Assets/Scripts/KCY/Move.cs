using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public float moveSpeed;
	public float jumpPower;
	public float recoilPower;
	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		PlayerMove();
	}


	private void PlayerMove()
	{
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		rb.MovePosition(rb.position + (new Vector3(x, 0, z) * Time.deltaTime * moveSpeed));

		if (Input.GetButtonDown("Jump"))
		{
			rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
		}

		if (Input.GetButtonDown("Fire1"))
		{
			rb.AddForce(Vector3.back * recoilPower, ForceMode.VelocityChange);
		}
	}


}
