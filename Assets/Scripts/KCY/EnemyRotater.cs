using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyRotater : MonoBehaviour
{

	private void Update()
	{

		Rotate();

	}

	private void Rotate()
	{
		Vector3 headDir = Vector3.zero;

		headDir = GameManager.Instance.player.transform.position - transform.position;

		transform.Rotate(headDir);
	}
}

