using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRan = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
	public bool fliseSpawner;
	private BoxCollider rangeColl;
	private EnemyPool enemyPool;

	private void Awake()
	{
		enemyPool = FindAnyObjectByType<EnemyPool>();
		rangeColl = GetComponent<BoxCollider>();
		if (fliseSpawner)
		{
			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}



	private void Start()
	{
		StartCoroutine(RandPosSpawn_Coroutine());


	}

	private IEnumerator RandPosSpawn_Coroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(2.2f);
			enemyPool.Pop(enemyPool.enemise[0].name);
			enemyPool.enemise[0].transform.position = GetSpawnPos();
			GameManager.Instance.flies.Add(enemyPool.enemise[0]);
			yield return new WaitUntil(() => GameManager.Instance.flies.Count < 16);
		}
	}




	private void FliesSpawn()
	{

	}

	private Vector3 GetSpawnPos()
	{
		Vector3 originPos = transform.position;

		float range_X = rangeColl.bounds.size.x;
		float range_Y = rangeColl.bounds.size.y;
		float range_Z = rangeColl.bounds.size.y;
		FliesMovement fliesMovement = enemyPool.enemise[0].GetComponentInChildren<FliesMovement>(); ;
		range_X = UniRan.Range((range_X / 2) * -1, range_X / 2);
		range_Y = UniRan.Range((range_Y / 2) * -1, range_Y / 2);
		range_Z = UniRan.Range((range_Z / 2) * -1, range_Z / 2);
		Vector3 randPos = new Vector3(range_X, range_Y, range_Z);
		fliesMovement.gizmoPoint.y = range_Y;
		Vector3 spawnPos = originPos + randPos;
		return spawnPos;
	}


	//private void FindColls()
	//{
	//	Collider[] colls = Physics.OverlapSphereNonAlloc
	//}



}
