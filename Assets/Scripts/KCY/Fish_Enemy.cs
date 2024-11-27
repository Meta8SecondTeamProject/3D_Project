using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Enemy : Enemy
{

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		isFly = false;
		base.Start();

		moveSpeed = DataManager.Instance.fishSpeed;
	}

	protected override void Update()
	{
		base.Update();
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

	protected override void OnCollisionEnter(Collision collision)
	{
		base.OnCollisionEnter(collision);
	}
}
