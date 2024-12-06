using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[Tooltip("0 : 파리\n1 : 물고기\n2 : 새\n3 : 폭탄 파리\n4 : 까마귀\n5. 총알")]
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
			GameObject newObj = Instantiate(originalPool[key]);
			newObj.name = key;
			targetPool.Add(newObj);
		}
		GameObject returnObj = targetPool[0];
		targetPool.RemoveAt(0);
		returnObj.gameObject.SetActive(true);
		returnObj.transform.SetParent(null);
		return returnObj;
	}
	public void Push(GameObject obj)
	{
		obj.SetActive(false);

		obj.transform.SetParent(transform);
		poolDic[obj.name].Add(obj);
		Debug.Log($"푸쉬된 옵젝 이름 : {obj.name} 활성화 유무 : {obj.activeSelf}");
	}
	public IEnumerator Push(GameObject obj, float t)
	{
		while (true)
		{
			yield return new WaitForSeconds(t);
			Push(obj);
		}
	}
}

