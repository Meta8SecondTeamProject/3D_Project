using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyRotater : MonoBehaviour
{

	private void Awake()
	{
	}

	private void Update()
	{
		Vector3 headDir = Vector3.zero;

		headDir = GameManager.Instance.transform.position - transform.position;

		Rotate(transform, headDir);

	}

	private void Rotate(Transform t, Vector3 dir)
	{
		t.Rotate(dir);
	}
}

