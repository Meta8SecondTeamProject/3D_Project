using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//StartScene���� new������ ������
//�ҷ��� ������ ����.
//������ �Ŵ����� ������ �ְ�
//public class GameStartManager : MonoBehaviour
//{
//    public void NewGame()
//    {
//        ResetData(); //�ʱ� �����͸� ����
//        //ContinueGame(); //�ʱ� �����͸� �ҷ�����

//        GameSaveData loadedData = SaveManager.LoadGame();

//        if (loadedData != null)
//        {
//            PlayerManager.Instance.LoadPlayerState(loadedData); //���� ����
//            LoadingSceneController.LoadScene(loadedData.currentSceneName); //�ε� ���� ���� �� ��ȯ
//        }
//    }

//    public void ContinueGame()
//    {
//        //JSON���� �б�
//        GameSaveData loadedData = SaveManager.LoadGame();

//        if (loadedData != null)
//        {
//            //�о���� ������ ������� �÷��̾� ���� �� �� �̵�
//            PlayerManager.Instance.LoadPlayerState(loadedData);
//            //SceneManager.LoadScene(loadedData.currentSceneName);

//            LoadingSceneController.LoadScene(loadedData.currentSceneName);
//            //PlayerManager.Instance.TransitionToScene(loadedData.currentSceneName);
//        }
//        else
//        {
//            Debug.Log("404 NOT FOUND, NO SAVED DATA");
//        }
//    }

//    private void ResetData()
//    {
//        GameSaveData resetData = new GameSaveData //������ ȣ��, �߰�ȣ �� ���ߵ�
//        {
//            playerPos = new Vector3(0, 70, 0), //�ʱ� ��ġ
//            health = 100, //�ʱ� ü��
//            ammo = 50,    //�ʱ� ź��
//            currentSceneName = "BBH_Scene" //�ʱ� �� 
//        };
//        SaveManager.SaveGame(resetData); //�ʱ�ȭ �� ���� JSON���� ��ȯ �� ����

//        Debug.Log("���̺� ������ �ʱ�ȭ��");
//    }
//}
