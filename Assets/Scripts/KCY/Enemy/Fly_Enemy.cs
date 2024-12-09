using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fly_Enemy : Enemy
{
	public GameObject bombEffect;
	public GameObject SpookRenderer;
	private CapsuleCollider capsuleCollider;
	public bool isBomb;
	//실제 콜라이더 붙어있는 곳이 아닌 부모 오브젝트 가져오는 용도
	protected override void Awake()
	{
		base.Awake();
		if (isBomb)
			capsuleCollider = GetComponent<CapsuleCollider>();
	}

	protected override void OnEnable()
	{
		if (isBoss == false)
		{
			base.OnEnable();
		}
		if (isBomb)
		{
			capsuleCollider.isTrigger = false;
			bombEffect.SetActive(false);
			SpookRenderer.SetActive(true);
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
				SpookRenderer.SetActive(false);
				bombEffect.SetActive(true);
				capsuleCollider.isTrigger = true;
				StartCoroutine(BombDistroy());
			}
		}
	}

	private IEnumerator BombDistroy()
	{
		while (true)
		{
			yield return new WaitForSeconds(100f);
			Destroy(gameObject);
		}
	}
}
