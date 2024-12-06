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

    //TODO : Potal �Լ��� �� �ٿ��ּ��� 
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
                    Debug.LogWarning("(Potal) ���� BossBird�� ����Ʈ�� ������ ����");
                break;

            case SceneNames.KCY_Scene:
                if (DataManager.Instance.data.isKilledBossFish)
                {
                    UIManager.Instance.TransitionToLoadScene(sceneName.ToString());
                    DataManager.Instance.Save();
                }
                else
                    Debug.LogWarning("(Potal) ���� BossFish�� ����Ʈ�� ������ ����");
                break;

            case SceneNames.BBH_Scene:
                DataManager.Instance.Save();
                UIManager.Instance.TransitionToLoadScene(sceneName.ToString());
                break;

            default:
                Debug.LogWarning("(Potal) �ùٸ��� ���� �ε����� ������");
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