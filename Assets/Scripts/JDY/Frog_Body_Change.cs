using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Body_Change : MonoBehaviour
{
	[Header("교체할 바디들 바디의 순서에 맞춰서"), Tooltip("기본바디, 상처 바디, 아머벨트차고 상처, 죽음, 모자, 벨트, 총기")]
	public GameObject[] bodys;

	private void Start()
	{
		bodys[6].SetActive(true);
		BodyChange();
	}

	[ContextMenu("바디체인지/이게맞나?")]
	public void BodyChange()
	{
		for (int i = 0; i < bodys.Length - 1; i++)
		{
			bodys[i].SetActive(false);
		}

		if (DataManager.Instance == null)
		{
			Debug.LogError("BodyChange/DataManager = null");
			return;
		}

		//모자...
		if (DataManager.Instance.data.isHat)
		{
			bodys[4].SetActive(true);
		}
		//벨트...
		if (DataManager.Instance.data.isAmmoBelt)
		{
			bodys[5].SetActive(true);
			if (DataManager.Instance.data.HP == 1)
			{
				bodys[2].SetActive(true);
				return;
			}
		}

		if (DataManager.Instance.data.HP == 1)
		{
			bodys[1].SetActive(true);
		}
		else if (DataManager.Instance.data.HP == 2)
		{
			bodys[0].SetActive(true);
		}
		else
		{
			bodys[3].SetActive(true);
		}
	}
}