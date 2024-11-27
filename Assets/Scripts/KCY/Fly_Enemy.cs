using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_Enemy : Enemy
{
	public bool isBomb;
	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		isFly = true;
		base.Start();

		moveSpeed = DataManager.Instance.fishSpeed;
	}

	protected override void Update()
	{
		base.Update();
		if (isBomb)
		{
			Vector3 moveDir = target.position - transform.position;
			Quaternion dirRot = Quaternion.LookRotation(moveDir.normalized);
			rb.rotation = Quaternion.Slerp(rb.rotation, dirRot, 1.5f * Time.deltaTime);
		}
		else
		{
			transform.LookAt(target);
		}
	}


	protected override void OnCollisionEnter(Collision collision)
	{

		base.OnCollisionEnter(collision);
		if (isBomb)
		{
			if (collision.collider.CompareTag("Player"))
			{
				Destroy(gameObject);
			}
		}
	}
}
