using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UniRan = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
	[Tooltip("0 : ÆÄ¸®\n1 : ¹°°í±â\n2 : »õ\n3 : ÆøÅº ÆÄ¸®\n4 : ±î¸¶±Í")]
	public int numberOfEnemy;
	private BoxCollider rangeColl;
	private EnemyPool enemyPool;
	private float bomb;
	private void Awake()
	{
		enemyPool = FindAnyObjectByType<EnemyPool>();
		rangeColl = GetComponent<BoxCollider>();
		switch (numberOfEnemy)
		{
			case 0:
				gameObject.SetActive(true);
				break;
			default:
				gameObject.SetActive(true);
				break;
		}
	}
	private void OnEnable()
	{

	}

	private void Start()
	{
		switch (numberOfEnemy)
		{
			case 0:
				StartCoroutine(RandPosSpawn_Coroutine(DataManager.Instance.fliesMaxCount));
				//Debug.Log(DataManager.Instance.fishMaxCount);
				break;
			case 1:
				StartCoroutine(RandPosSpawn_Coroutine(DataManager.Instance.fishMaxCount));
				//Debug.Log(DataManager.Instance.fishMaxCount);
				break;
			case 2:
				StartCoroutine(RandPosSpawn_Coroutine(DataManager.Instance.birdMaxCount));
				break;
			default:
				//Debug.LogWarning("±×·± ³ð ¾ø´Ù.");
				break;
		}
	}

	private IEnumerator RandPosSpawn_Coroutine(int enemyMaxCount)
	{
		while (true)
		{
			if (numberOfEnemy == 0)
			{
				bomb = UniRan.Range(0f, 1f);
				if (bomb > 0.9f) numberOfEnemy = 3;
			}
			enemyPool.Pop(enemyPool.enemise[numberOfEnemy].name);
			enemyPool.enemise[numberOfEnemy].transform.position = GetSpawnPos();
			if (numberOfEnemy == 3) numberOfEnemy = 0;
			GameManager.Instance.enemy[numberOfEnemy].Add(enemyPool.enemise[numberOfEnemy]);
			yield return null;
			//EditorApplication.isPaused = true;
			yield return new WaitUntil(() => GameManager.Instance.enemy[numberOfEnemy].Count < enemyMaxCount);
		}
	}



	private Vector3 GetSpawnPos()
	{
		Vector3 originPos = transform.position;

		float range_X = rangeColl.bounds.size.x;
		float range_Y = rangeColl.bounds.size.y;
		float range_Z = rangeColl.bounds.size.y;
		range_X = UniRan.Range((range_X / 2) * -1, range_X / 2);
		range_Y = UniRan.Range((range_Y / 2) * -1, range_Y / 2);
		range_Z = UniRan.Range((range_Z / 2) * -1, range_Z / 2);
		Vector3 randPos = new Vector3(range_X, range_Y, range_Z);
		Vector3 spawnPos = originPos + randPos;
		return spawnPos;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (numberOfEnemy == 4 && other.CompareTag("Player"))
		{
			StartCoroutine(RandPosSpawn_Coroutine(DataManager.Instance.birdBlackMaxCount));
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (numberOfEnemy == 4 && other.CompareTag("Player"))
		{
			StopCoroutine(RandPosSpawn_Coroutine(DataManager.Instance.birdBlackMaxCount));
		}
	}

	//private void FindColls()
	//{
	//	Collider[] colls = Physics.OverlapSphereNonAlloc
	//}



}
