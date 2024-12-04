using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManager<GameManager>
{

	public Player player;
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
		PlayerInstantiate();
	}

	private void Start()
	{
		//������ ���ѿ� �ڵ�
		Application.targetFrameRate = 60;

		//�ӽ÷� ���� ã��
		//pool = FindAnyObjectByType<ObjectPool>();
		//player = FindAnyObjectByType<Player>();
	}

	private Vector3 spawnPos;
	[Header("������ ������ ������")]
	public GameObject playerPrefab;
	public void PlayerInstantiate()
	{

		spawnPos = DataManager.Instance.StartPosition();

		if (playerPrefab != null)
		{
			Instantiate(playerPrefab).transform.position = spawnPos;
			player = playerPrefab.GetComponent<Player>();
		}
	}
	public void FindPool()
	{
		SceneManager.sceneLoaded += (x, y) =>
		{
			pool = FindAnyObjectByType<ObjectPool>();
		};
	}
}
