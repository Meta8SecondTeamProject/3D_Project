using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

	protected Rigidbody rb;
	protected bool isFly;
	protected float moveSpeed;
	protected Transform target;

	Vector3 velocity = new Vector3(1, 1, 1);



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
		Vector3 moveDir = target.position - transform.position;
		Move(moveDir.normalized);
	}

	protected virtual void Move(Vector3 dir)
	{
		rb.AddForce(dir * moveSpeed);
	}





	protected virtual void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Projectile"))
		{
			Destroy(gameObject);
		}
	}


}
