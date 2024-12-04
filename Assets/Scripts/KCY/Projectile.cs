using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	private void OnEnable()
	{
		StartCoroutine(GameManager.Instance.pool.Push(this.gameObject, 5f));
	}

}
