using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //���� ����¿� ���


//Json�� ���� �����͵��� ���̺�/�ε��ϴ� ģ��
//�����͸Ŵ����� ������ ���� �ʰ� ���� ����...
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
    public static void SaveGame(Data data)
    {
        string json = JsonUtility.ToJson(data, true); //GameSaveData ��ü�� JSON ���ڿ��� ��ȯ
        //�� ��° ���ڷ� ���� bool���� �鿩���� ���θ� ��Ÿ��, true�� �ϸ� ����� ���� ���� ���·� ��ȯ������ �׸�ŭ �뷮�� �� �þ
        //false�� �ϸ� �鿩���� ���� ����� ������ JSON ���ڿ��� ��ȯ�ϴϱ� ����ũ�Ⱑ �۰� ���� ������ ������ �� ����
        File.WriteAllText(SavePath, json); //��ȯ�� JSON�� ���Ϸ� ����
        Debug.Log("GameSaved :" + SavePath);
    }

    //�����͸� �ҷ����� �޼���
    public static Data LoadGame()
    {
        if (File.Exists(SavePath)) //������ �����ϴ��� Ȯ��
        {
            string json = File.ReadAllText(SavePath); //���Ͽ��� JSON�б�
            Debug.Log("Loaded JSON: " + json);
            Data data = JsonUtility.FromJson<Data>(json); //JSON���ڿ��� GameSaveData��ü�� ��ȯ
            //Debug.Log("Game Loaded" + SavePath);
            return data;
        }
        else
        {
            #region ������ ���� ���� ����ϴ°͵� �����ϴٴ� ����
            //�������µ� ������ ���� ������ �ǹ��Ⱦ��
            Debug.Log("������ �����Ƿ� ���� �����մϴ�.");
            string defaultJson = "{\r\n    \"currentSceneName\": \"BBH_Scene\",\r\n    \"HP\": 2,\r\n    \"currentHP\": 2,\r\n    \"ammo\": 4,\r\n    \"currentAmmo\": 4,\r\n    \"maxAmmo\": 16,\r\n    \"money\": 0,\r\n    \"isHat\": false,\r\n    \"isAmmoBelt\": false,\r\n    \"isDoubleJump\": false,\r\n    \"isClear\": false,\r\n    \"isHardClear\": false\r\n}";
            File.WriteAllText(SavePath, defaultJson);
            Data data = JsonUtility.FromJson<Data>(defaultJson);
            return data;
            #endregion
            //Debug.Log("404 NOT FOUND");
            //return null; //������ ������ null ��ȯ

        }
    }
}
