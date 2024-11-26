using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
	public Enemy[] enemise;
	public Dictionary<string, Enemy> originalPool = new();
	public Dictionary<string, List<Enemy>> poolDic = new();


	public void Awake()
	{
		for (int i = 0; i < enemise.Length; i++)
		{
			originalPool.Add(enemise[i].name, enemise[i]);
			poolDic.Add(enemise[i].name, new());
		}
	}

	public Enemy Pop(string key)
	{
		List<Enemy> targetPool = poolDic[key];
		if (targetPool.Count <= 0)
		{
			Enemy newEnemy = Instantiate(originalPool[key]);
			newEnemy.name = key;
			targetPool.Add(newEnemy);
		}
		Enemy returnEnemy = targetPool[0];
		targetPool.RemoveAt(0);
		returnEnemy.gameObject.SetActive(true);
		returnEnemy.transform.SetParent(null);
		return returnEnemy;
	}
	public void Push(Enemy enemy)
	{
		enemy.gameObject.SetActive(false);
		enemy.transform.SetParent(transform);
		poolDic[enemy.name].Add(enemy);
	}
}

