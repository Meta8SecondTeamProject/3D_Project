using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum SceneNames { BBH_Scene, JDY_Scene, KCY_Scene }
public enum BossKilled { killedBossBird, killedBossFish }
public class Potal : MonoBehaviour
{
    public SceneNames sceneName;
    public BossKilled bossKilled;

    //TODO : Potal 함수로 좀 줄여주세요 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == false)
            return;

        switch (sceneName)
        {
            case SceneNames.JDY_Scene:
                if (DataManager.Instance.data.isKilledBossBird)
                {
                    UIManager.Instance.TransitionToLoadScene(sceneName.ToString());
                    DataManager.Instance.Save();
                }
                else
                    Debug.LogWarning("(Potal) 아직 BossBird를 쓰러트린 전적이 없음");
                break;

            case SceneNames.KCY_Scene:
                if (DataManager.Instance.data.isKilledBossFish)
                {
                    UIManager.Instance.TransitionToLoadScene(sceneName.ToString());
                    DataManager.Instance.Save();
                }
                else
                    Debug.LogWarning("(Potal) 아직 BossFish를 쓰러트린 전적이 없음");
                break;

            case SceneNames.BBH_Scene:
                DataManager.Instance.Save();
                UIManager.Instance.TransitionToLoadScene(sceneName.ToString());
                break;

            default:
                Debug.LogWarning("(Potal) 올바르지 않은 인덱스에 접근중");
                break;
        }
    }
}

//      if(CompareTag("Player") && DataManager.Instance.data.isKilledBossBird

//      if (other.CompareTag("Player"))
//{
//	DataManager.Instance.Save();
//	
//}