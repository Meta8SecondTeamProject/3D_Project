using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //���� ����¿� ���


public class SaveManager : MonoBehaviour
{
    //���� ���
    //��Ȯ���� C:\Users\�����\AppData\LocalLow\DefaultCompany\3D_Project�� savefile.json���� ������
    private static string SavePath => Application.persistentDataPath + "/savefile.json";
    //SaveManager�� �ν��Ͻ��� �Ź� �����ϴ°� ���� static���� �����ϰ� ����ϴ°� �� ������

    //private static string SavePath { get { return Application.persistentDataPath + "/savefile.json"; } }
    //���ٽ����� ��������� �̷��� �ᵵ �Ȱ���
    //��ȯ ���� �����ϴ� ������ ���������� =>�� �� �������̰� �������� ���ٰ� ��

    //�����͸� �����ϴ� �޼���
    public static void SaveGame(GameSaveData data)
    {
        string json = JsonUtility.ToJson(data, true); //GameSaveData ��ü�� JSON ���ڿ��� ��ȯ
        //�� ��° ���ڷ� ���� bool���� �鿩���� ���θ� ��Ÿ��, true�� �ϸ� ����� ���� ���� ���·� ��ȯ������ �׸�ŭ �뷮�� �� �þ
        //false�� �ϸ� �鿩���� ���� ����� ������ JSON ���ڿ��� ��ȯ�ϴϱ� ����ũ�Ⱑ �۰� ���� ������ ������ �� ����
        File.WriteAllText(SavePath, json); //��ȯ�� JSON�� ���Ϸ� ����
        //Debug.Log("GameSaved :" + SavePath);
    }

    //�����͸� �ҷ����� �޼���
    public static GameSaveData LoadGame()
    {
        if (File.Exists(SavePath)) //������ �����ϴ��� Ȯ��
        {
            string json = File.ReadAllText(SavePath); //���Ͽ��� JSON�б�
            //Debug.Log("Loaded JSON: " + json);
            GameSaveData data = JsonUtility.FromJson<GameSaveData>(json); //JSO���ڿ��� GameSaveData��ü�� ��ȯ
            //Debug.Log("Game Loaded" + SavePath);
            return data;
        }
        else
        {
            #region ������ ���� ���� ����ϴ°͵� �����ϴٴ� ����
            //Debug.Log("������ �����Ƿ� ���� �����մϴ�.");
            //string defaultJson = "{\"health\":100,\"ammo\":50,\"currentSceneName\":\"StartScene\"}";
            //File.WriteAllText(savePath, defaultJson);
            #endregion
            Debug.Log("404 NOT FOUND");
            return null; //������ ������ null ��ȯ
        }
    }
}
