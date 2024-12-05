using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[Tooltip("0 : ÆÄ¸®\n1 : ¹°°í±â\n2 : »õ\n3 : ÆøÅº ÆÄ¸®\n4 : ±î¸¶±Í\n5. ÃÑ¾Ë")]
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
		obj.gameObject.SetActive(false);
		obj.transform.SetParent(transform);
		poolDic[obj.name].Add(obj);
		Debug.Log($"¿ÉÁ§ ÀÌ¸§ : {obj.name}");
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

