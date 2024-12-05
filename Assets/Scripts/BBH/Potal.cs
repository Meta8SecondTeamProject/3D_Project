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
			Debug.Log("포탈 진입");
			//이거 꼭 필요한가요 어짜피 씬 넘어가면 Destroy되는데 괜히 게임창만 이상해지는거같음
			//TODO : 일단 주석처리후 필요하면 다시 주석 해제 요망
			//other.gameObject.SetActive(false);
			DataManager.Instance.Save();
			UIManager.Instance.TransitionToLoadScene(sceneName.ToString());
		}
	}
}
