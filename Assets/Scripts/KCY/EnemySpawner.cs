using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public bool fliseSpawner;


	private void Start()
	{
		if (fliseSpawner)
		{
			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}




	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position, new Vector3(150, 40, 150));
	}

}
