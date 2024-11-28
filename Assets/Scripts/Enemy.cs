using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

	protected Rigidbody rb;
	protected bool isFly;
	protected float moveSpeed;
	protected Transform target;
	protected Vector3 moveDir;
	public Transform attackSpot;

	protected virtual void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	protected virtual void Start()
	{
		target = GameManager.Instance.player.transform;
		if (isFly == false) { rb.useGravity = true; }
		else { rb.useGravity = false; }
	}


	protected virtual void Update()
	{
		moveDir = target.position - attackSpot.transform.position;
	}

	protected virtual void Move(Vector3 dir)
	{
		rb.AddForce(dir * moveSpeed);
	}

	protected virtual void Look(Vector3 dir, float rotVal)
	{
		Quaternion dirRot = Quaternion.LookRotation(dir);
		rb.rotation = Quaternion.Slerp(rb.rotation, dirRot, rotVal * Time.deltaTime);
	}



	protected virtual void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Projectile"))
		{
			Destroy(gameObject);
		}
	}


}
