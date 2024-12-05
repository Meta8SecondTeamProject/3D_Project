using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRan = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
	[Tooltip("0 : �ĸ�\n1 : �����\n2 : ��\n3 : ��ź �ĸ�\n4 : ���")]
	public int numberOfEnemy;
	private BoxCollider rangeColl;
	private ObjectPool enemyPool;
	private float bomb;

	[Header("Bird, Fish, Toad")]
	public GameObject[] bossEnemy;

	private void Awake()
	{
		enemyPool = FindAnyObjectByType<ObjectPool>();
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

	private void Start()
	{
		switch (numberOfEnemy)
		{
			case 0:
				StartCoroutine(RandPosSpawn_Coroutine(DataManager.Instance.fliesMaxSpawnCount));
				//Debug.Log(DataManager.Instance.fishMaxCount);
				break;
			case 1:
				StartCoroutine(RandPosSpawn_Coroutine(DataManager.Instance.fishMaxSpawnCount));
				//Debug.Log(DataManager.Instance.fishMaxCount);
				break;
			case 2:
				StartCoroutine(RandPosSpawn_Coroutine(DataManager.Instance.birdMaxSpawnCount));
				Debug.Log(DataManager.Instance.birdMaxSpawnCount);
				break;
			default:
				//Debug.LogWarning("�׷� �� ����.");
				break;
		}
	}

	private IEnumerator RandPosSpawn_Coroutine(int enemyMaxCount)
	{
		while (true)
		{
			Debug.Log($"�ִ� Fish ���� ī��Ʈ : {DataManager.Instance.fishMaxSpawnCount}");
			Debug.Log($"�� ������ üũ�� : {GameManager.Instance.enemy[numberOfEnemy].Count} < {enemyMaxCount}");
			//Debug.Log($"Ȱ��ȭ�� Ʈ���� �� : {DataManager.Instance.triggerOn}");
			if (numberOfEnemy == 0)
			{
				bomb = UniRan.Range(0f, 1f);
				if (bomb > 0.9f) numberOfEnemy = 3;
			}
			// TODO : ���� �ּ� �����ؼ� Ʈ���� 3�� �̻� Ȱ��ȭ�Ǹ� Enemy �����ǰ�,
			//if (DataManager.Instance.triggerOn >= 3)
			//{
			enemyPool.Pop(enemyPool.obj[numberOfEnemy].name);
			enemyPool.obj[numberOfEnemy].transform.position = GetSpawnPos();
			if (numberOfEnemy == 3) numberOfEnemy = 0;
			yield return null;
			//EditorApplication.isPaused = true;

			Debug.Log(GameManager.Instance.enemy[numberOfEnemy].Count);
			yield return new WaitUntil(() => GameManager.Instance.enemy[numberOfEnemy].Count < enemyMaxCount);
			//TODO : ��ǥ Enemy���� ä��� ���� Enemy�� �����ǵ��� ����,
			//       ���� ���ϵ��ڵ��ε� �̰� ��ĥ��� �ƴ»�� ��,
			//       �ε����� ���� �� �ϴ� �ϱ� �ؾ��ϴµ� ����������
			//if(DataManager.Instance.birdMaxCount > DataManager.Instance.birdKillCount)
			//{
			//	SpawnBoss();
			//}
			//}
		}
	}


	private void SpawnBoss()
	{
		//TODO : SapwnBoss���� �ϵ��ڵ�, ������ ����
		int sceneIndex = SceneManager.GetActiveScene().buildIndex;
		GameObject boss;

		switch (sceneIndex)
		{
			case 3: //BBH_Scene
				boss = Instantiate(bossEnemy[0]);
				boss.transform.position = GetSpawnPos();
				boss.transform.rotation = Quaternion.identity;
				break;
			case 4: //JDY_Scene
				boss = Instantiate(bossEnemy[0]);
				boss.transform.position = GetSpawnPos();
				boss.transform.rotation = Quaternion.identity;
				break;
			default:
				break;
		}
		//TODO : ���� ������ ��Ż Ȱ��ȭ�ǵ��� bool������ ���� �߰��ϱ�, ���� ������ �����Ǿ��ϴ� DataManager�� �߰�
		// 

	}
	private Vector3 GetSpawnPos()
	{
		Vector3 originPos = transform.position;

		float range_X = rangeColl.bounds.size.x;
		float range_Y = rangeColl.bounds.size.y;
		float range_Z = rangeColl.bounds.size.z;
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



}
