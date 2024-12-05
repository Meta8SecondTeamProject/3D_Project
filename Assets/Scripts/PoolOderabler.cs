using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolOderabler : MonoBehaviour
{
	public bool goPool;
	public GameObject m_Enemy;


	private void OnEnable()
	{
		goPool = false;
		m_Enemy.gameObject.SetActive(true);
	}

	private void Update()
	{
		if (goPool)
		{
			GameManager.Instance.pool.Push(gameObject, 3);
			goPool = false;
		}
	}
}
