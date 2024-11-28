using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("AttackPos"))
		{
			if (Knockback(collision, out Rigidbody otherRb))
			{
				otherRb.AddForce(Vector3.back * 20f, ForceMode.Impulse);
				print("¾Æ¾ß!");
			}

		}
		print(collision.gameObject.name);
	}

	private bool Knockback(Collision collision, out Rigidbody otherRb)
	{
		otherRb = collision.collider.GetComponentInParent<Rigidbody>();
		return true;
	}
}
