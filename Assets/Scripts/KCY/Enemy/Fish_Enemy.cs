using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Enemy : Enemy
{
	public float jumpCooldown;
	public float jumpForce;
	public float forceGravity;
	private bool isWater = false;

	protected override void Awake()
	{
		base.Awake();
	}
	protected override void OnEnable()
	{
		if (isBoss == false)
		{
			base.OnEnable();
		}
	}

	protected override void Start()
	{
		isFly = false;
		base.Start();
		StartCoroutine(Jump());
		if (isBossFish)
		{
			moveSpeed = DataManager.Instance.fishBossSpeed;
		}
		else
		{
			moveSpeed = DataManager.Instance.fishSpeed;
		}
	}

	private IEnumerator Jump()
	{
		while (true)
		{
			yield return new WaitForSeconds(jumpCooldown);
			yield return new WaitUntil(() => isWater);
			jumpForce += Vector3.Distance(GameManager.Instance.player.transform.position, transform.position);
			rb.AddForce(moveDir.normalized * jumpForce, ForceMode.Impulse);

		}
	}
	private void FixedUpdate()
	{
		if (transform.position.y > 10)
			rb.AddForce(Vector3.down * forceGravity);
	}
	protected override void Update()
	{
		base.Update();
		Move(moveDir.normalized);
		Look(moveDir, 5f);
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Water"))
		{
			rb.useGravity = false;
			isWater = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Water"))
		{
			rb.useGravity = true;
			isWater = false;
		}
	}

}
