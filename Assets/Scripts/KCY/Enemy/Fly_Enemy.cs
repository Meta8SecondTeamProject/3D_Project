using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fly_Enemy : Enemy
{

	public bool isBomb;
	//���� �ݶ��̴� �پ��ִ� ���� �ƴ� �θ� ������Ʈ �������� �뵵
	protected override void OnEnable()
	{
		if (isBoss == false)
		{
			base.OnEnable();
		}
	}
	protected override void Start()
	{
		isFly = true;
		base.Start();
		if (isBomb)
			moveSpeed = DataManager.Instance.bombFliesSpeed;
		else
			moveSpeed = DataManager.Instance.birdSpeed;
	}

	protected override void Update()
	{
		base.Update();
		if (isBomb)
		{
			Look(moveDir, 1.5f);
			Move(moveDir.normalized);
		}
		else
		{
			Look(moveDir, 5f);
			Move(moveDir.normalized);
		}
	}


	protected override void OnCollisionEnter(Collision collision)
	{

		base.OnCollisionEnter(collision);

		if (isBomb)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				GameManager.Instance.player.TakeDamage(true);
				Destroy(gameObject);
			}
		}
	}
}
