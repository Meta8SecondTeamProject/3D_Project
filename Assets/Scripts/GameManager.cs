using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManager<GameManager>
{

	public Player player;
	public GameObject playerObj;
	public List<GameObject>[] enemy = new List<GameObject>[5];
	public ObjectPool pool;
	//TODO : �� ����Ʈ �߰� ����
	protected override void Awake()
	{
		base.Awake();
		for (int i = 0; i < enemy.Length; i++)
		{
			enemy[i] = new List<GameObject>();
		}

		//�ӽ������� �ּ�ó��
		//PlayerInstantiate();
	}

	private void Start()
	{


		SceneManager.sceneLoaded += (x, y) =>
		{
			UIManager.Instance.GameSceneTextUpdate();
			pool = FindAnyObjectByType<ObjectPool>();
			Cursor.lockState = CursorLockMode.Locked;
		};
		//������ ���ѿ� �ڵ�
		//Application.targetFrameRate = 60;

		//�ӽ÷� ���� ã��
		//pool = FindAnyObjectByType<ObjectPool>();
		//player = FindAnyObjectByType<Player>();


	}

	private Vector3 spawnPos;
	[Header("������ ������ ������")]
	public GameObject playerPrefab;
	public void PlayerInstantiate()
	{
		if (SceneManager.GetActiveScene().name != "GameStartScene")
		{
			spawnPos = DataManager.Instance.StartPosition();
			Debug.Log(spawnPos);
			if (playerPrefab != null)
			{
				Debug.Log("����");
				//TODO : ���� ���� �� �������
				pool = FindAnyObjectByType<ObjectPool>();
				playerObj = Instantiate(playerPrefab);
				playerObj.transform.position = spawnPos;
				player = playerObj.GetComponent<Player>();
			}
		}
	}

	public void EnemyToPool(int index)
	{
		if (pool == null)
		{
			Debug.LogError("GameManager / ObjcetPool / null Error");
			return;
		}

		List<GameObject> pushEnemys = enemy[index];

		foreach (var pushEnemy in pushEnemys)
		{
			if (pushEnemy == null) continue;
			pool.Push(pushEnemy);
		}
		enemy[index].Clear();
	}
}

//interface IBuiable
//{
//	public int BuySometing(ISellable seller);
//}

//interface ISellable
//{
//	public int SellSometing(IBuiable customer);
//	public int SellPrice { get; set; }
//}