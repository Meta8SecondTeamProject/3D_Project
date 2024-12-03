using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{

    [Header("생성할 개구리 프리팹")] internal Player player;
    public List<GameObject>[] enemy = new List<GameObject>[5];

    //TODO : 적 리스트 추가 예정
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
