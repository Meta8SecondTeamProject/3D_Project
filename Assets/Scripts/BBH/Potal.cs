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
        //TODO : 보스몬스터 잡아야 포탈이 활성화되도록 로직 변경
        //e.g : if(CompareTag("Player") && DataManager.Instance.Data.isKilledBossBird && sceneName.ToString() == "JDY_Scene")
        //			UIManager.Instance.TransitionToLoadScene(sceneName.ToString());
		//씬 이름 체크부분이 뭔가 이상한거같은데 엄
        if (other.CompareTag("Player"))
		{


			Debug.Log("포탈 진입");
			//이거 꼭 필요한가요 어짜피 씬 넘어가면 Destroy되는데 괜히 게임창만 이상해지는거같음
			//TODO : 상의후 필요하면 다시 주석 해제 요망
			//other.gameObject.SetActive(false);
			DataManager.Instance.Save();
			UIManager.Instance.TransitionToLoadScene(sceneName.ToString());
		}
	}
}
