using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum SceneNames { BBH_Scene, JDY_Scene, KCY_Scene }
public class Potal : MonoBehaviour
{
	public SceneNames sceneName;
	//public Vector3 arrivePos;
	private void OnTriggerEnter(Collider other)
	{

		if (other.CompareTag("Player"))
		{
			Debug.Log("��Ż ����");
			//�̰� �� �ʿ��Ѱ��� ��¥�� �� �Ѿ�� Destroy�Ǵµ� ���� ����â�� �̻������°Ű���
			//TODO : �ϴ� �ּ�ó���� �ʿ��ϸ� �ٽ� �ּ� ���� ���
			//other.gameObject.SetActive(false);
			DataManager.Instance.Save();
			UIManager.Instance.TransitionToLoadScene(sceneName.ToString());
		}
	}
}
