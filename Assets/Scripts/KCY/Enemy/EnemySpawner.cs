using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRan = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
	[Tooltip("0 : 파리\n1 : 물고기\n2 : 새\n3 : 폭탄 파리\n4 : 까마귀")]
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
				//Debug.LogWarning("그런 놈 없다.");
				break;
		}
	}

	private IEnumerator RandPosSpawn_Coroutine(int enemyMaxCount)
	{
		while (true)
		{
			Debug.Log($"최대 Fish 스폰 카운트 : {DataManager.Instance.fishMaxSpawnCount}");
			Debug.Log($"비교 연산자 체크용 : {GameManager.Instance.enemy[numberOfEnemy].Count} < {enemyMaxCount}");
			//Debug.Log($"활성화된 트리거 수 : {DataManager.Instance.triggerOn}");
			if (numberOfEnemy == 0)
			{
				bomb = UniRan.Range(0f, 1f);
				if (bomb > 0.9f) numberOfEnemy = 3;
			}
			// TODO : 추후 주석 해제해서 트리거 3개 이상 활성화되면 Enemy 스폰되게,
			//if (DataManager.Instance.triggerOn >= 3)
			//{
			enemyPool.Pop(enemyPool.obj[numberOfEnemy].name);
			enemyPool.obj[numberOfEnemy].transform.position = GetSpawnPos();
			if (numberOfEnemy == 3) numberOfEnemy = 0;
			yield return null;
			//EditorApplication.isPaused = true;

			Debug.Log(GameManager.Instance.enemy[numberOfEnemy].Count);
			yield return new WaitUntil(() => GameManager.Instance.enemy[numberOfEnemy].Count < enemyMaxCount);
			//TODO : 목표 Enemy량을 채우면 보스 Enemy가 스폰되도록 설정,
			//       지금 쌉하드코딩인데 이거 고칠방법 아는사람 손,
			//       인덱스를 쓰던 뭘 하던 하긴 해야하는데 으으윽흑흑
			//if(DataManager.Instance.birdMaxCount > DataManager.Instance.birdKillCount)
			//{
			//	SpawnBoss();
			//}
			//}
		}
	}


	private void SpawnBoss()
	{
		//TODO : SapwnBoss로직 하드코딩, 상의후 결정
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
		//TODO : 보스 잡으면 포탈 활성화되도록 bool변수던 뭐던 추가하기, 다음 씬에도 연동되야하니 DataManager에 추가
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
