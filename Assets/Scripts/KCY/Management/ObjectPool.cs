using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[Tooltip("0 : �ĸ�\n1 : �����\n2 : ��\n3 : ��ź �ĸ�\n4 : ���\n5. �Ѿ�")]
	public GameObject[] obj;
	public Dictionary<string, GameObject> originalPool = new();
	public Dictionary<string, List<GameObject>> poolDic = new();


	public void Awake()
	{
		for (int i = 0; i < obj.Length; i++)
		{
			originalPool.Add(obj[i].name, obj[i]);
			poolDic.Add(obj[i].name, new());
		}
	}

	public GameObject Pop(string key)
	{
		List<GameObject> targetPool = poolDic[key];
		if (targetPool.Count <= 0)
		{
			GameObject newEnemy = Instantiate(originalPool[key]);
			newEnemy.name = key;
			targetPool.Add(newEnemy);
		}
		GameObject returnEnemy = targetPool[0];
		targetPool.RemoveAt(0);
		returnEnemy.gameObject.SetActive(true);
		returnEnemy.transform.SetParent(null);
		return returnEnemy;
	}
	public void Push(GameObject enemy)
	{
		enemy.gameObject.SetActive(false);
		enemy.transform.SetParent(transform);
		poolDic[enemy.name].Add(enemy);
	}
}

