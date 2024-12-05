using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

	public Frog_Move frogMove;
	public Frog_Look frogLook;
	public Frog_Action frogAction;

	private void Awake()
	{
		frogLook = GetComponent<Frog_Look>();
		frogMove = GetComponent<Frog_Move>();
		frogAction = GetComponent<Frog_Action>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Enemy"))
		{
			if (Knockback(collision, out Rigidbody otherRb))
			{
				otherRb.AddForce(Vector3.back * 20f, ForceMode.Impulse);
				DataManager.Instance.data.currentHP--;
			}
		}
	}

	private bool Knockback(Collision collision, out Rigidbody otherRb)
	{
		otherRb = collision.collider.GetComponentInParent<Rigidbody>();
		return true;
	}
}
