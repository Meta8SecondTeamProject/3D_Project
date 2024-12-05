using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Enemy : Enemy
{
	public float jumpCooldown;
	public float jumpForce;
	public float forceGravity;
	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		isFly = false;
		base.Start();
		StartCoroutine(Jump());
		moveSpeed = DataManager.Instance.fishSpeed;
	}

	private IEnumerator Jump()
	{
		while (true)
		{

			yield return new WaitForSeconds(jumpCooldown);
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

}
