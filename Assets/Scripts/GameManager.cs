using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{

    [Header("������ ������ ������")] internal Player player;
    public List<GameObject>[] enemy = new List<GameObject>[5];

    //TODO : �� ����Ʈ �߰� ����
    protected override void Awake()
    {
        base.Awake();
        player = FindAnyObjectByType<Player>();
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i] = new List<GameObject>();
        }
    }

    private  Vector3 spawnPos;
    public GameObject playerPrefab;
    public void PlayerInstantiate()
    {
        spawnPos = DataManager.Instance.StartPosition();

        if (playerPrefab != null)
            Instantiate(playerPrefab).transform.position = spawnPos;
    }
}
