using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	private Rigidbody rb;


	public bool isFlies = false;
	public float moveSpeed;
	public Transform target;





	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		target = GameManager.Instance.player.transform;

	}


	private void Update()
	{
		if (!isFlies)
		{
			Vector3 moveDir = target.position - transform.position;
			Move(moveDir.normalized);
			transform.LookAt(target);
		}
	}

	private void Move(Vector3 dir)
	{
		Vector3 movePos = rb.position + (dir * moveSpeed * Time.fixedDeltaTime);
		rb.MovePosition(movePos);
	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.collider.CompareTag("Water"))
		{
			rb.useGravity = false;
		}
		if (collision.collider.CompareTag("Ground"))
		{
			rb.useGravity = true;
		}
	}


	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Projectile"))
		{
			Destroy(gameObject);
		}
	}


}
