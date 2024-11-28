using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{

	public GameObject triggerOn;
	public GameObject triggerOff;
	private void Awake()
	{
		triggerOn.SetActive(false);
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Projectile"))
		{
			triggerOff.SetActive(false);
			triggerOn.SetActive(true);
		}
	}
}
