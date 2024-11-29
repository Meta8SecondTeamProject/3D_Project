using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	public BoxCollider walls;

	private void Awake()
	{

	}
	private void Start()
	{
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(walls.transform.position, walls.transform.localScale);

	}
}

