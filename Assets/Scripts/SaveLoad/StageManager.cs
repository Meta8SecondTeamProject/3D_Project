using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�� ������ ���� ��ġ�� �������ִ� ģ��
//�����͸Ŵ����� ���ӸŴ������� ������� ���� ������
//������ġ�� ���ӸŴ����� �����ϰԲ�.
//public class StageManager : MonoBehaviour
//{
//    public Vector3 spawnPos; //�÷��̾� �ʱ� ��ġ ����

//    private void Start()
//    {

//        //����� ��ġ�� �ִ��� Ȯ��
//        GameSaveData saveData = SaveManager.LoadGame();

//        //���̺� �����Ͱ� �����ϰ�, ���̺� �������� �� �̸��� ���� �� �̸��� ��ġ�Ѵٸ�
//        if (saveData != null && saveData.currentSceneName == SceneManager.GetActiveScene().name)
//        {
//            //���̺� �������� ����� �����ǿ� �÷��̾ ������
//            PlayerManager.Instance.CreatePlayer(saveData.playerPos);
//            Debug.Log($"���̺� �����Ϳ��� �ҷ��� ��ġ�� �̵���Ŵ : {saveData.playerPos}");
//        }
//        else
//        {
//            //�װ� �ƴϸ� �̸� ���ص� ��ġ�� ����
//            PlayerManager.Instance.CreatePlayer(spawnPos);
//            Debug.Log($"�⺻ ��ġ�� �̵���Ŵ : {spawnPos}");
//        }
//    }
//}

