using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_Enemy : Enemy
{
	public bool isBat;
	public bool isBomb;
	public bool isBird;
	//실제 콜라이더 붙어있는 곳이 아닌 부모 오브젝트 가져오는 용도
	public FliesMovement batMoveMent;
	protected override void Awake()
	{
		base.Awake();
		if (isBat)
			batMoveMent = GetComponentInParent<FliesMovement>();
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


	protected override void OnTriggerEnter(Collider collision)
	{
		base.OnTriggerEnter(collision);

	}
	private void OnCollisionEnter(Collision collision)
	{
		if (isBomb)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				DataManager.Instance.data.currentHP--;
				GameManager.Instance.player.TakeDamage();
				GameManager.Instance.pool.Push(gameObject);
			}
		}
	}
}
