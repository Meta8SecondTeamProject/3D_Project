using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
	public GameObject[] fracture;

	private void OnEnable()
	{
		StartCoroutine(GameManager.Instance.pool.Push(this.gameObject, 3));
	}

	private void OnDisable()
	{
		foreach (var f in fracture)
		{
			f.transform.position = transform.position;
		}
	}
}
