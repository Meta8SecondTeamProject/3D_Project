using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public GameObject playerPrefab;
    private GameObject playerInstance;

    public int health = 2;
    public int ammo = 4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; //�̱��� �ʱ�ȭ
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public void CreatePlayer(Vector3 pos)
    { 
        #region �÷��̾� ���� ���� ����
        string currentScene = SceneManager.GetActiveScene().name;

        //GameStartScene������ �÷��̾� �������� ����, ��� ȣ���� ������ ����
        if (currentScene == "GameStartScene")
        {
            Debug.Log("Player creation skipped in GameStartScene");
            return;
        }
        #endregion

        if (playerInstance == null)
        {
            //�÷��̾� �������� �̿��� �÷��̾ �����ϰ�,
            //�÷��̾��� ��ġ�� �̵�������
            playerInstance = Instantiate(playerPrefab);
            playerInstance.transform.position = pos;
            Debug.Log($"�÷��̾ �����ϰ� �̵���Ŵ {pos}");
        }
        else
        {
            playerInstance.transform.position = pos;
            Debug.Log($"�÷��̾ �̵���Ŵ {pos}");
        }
    }

    #region �׽�Ʈ��
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            //SceneManager.LoadScene("BBH_Scene");
            LoadingSceneController.LoadScene("BBH_Scene");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            //SceneManager.LoadScene("KCY_Scene");
            LoadingSceneController.LoadScene("KCY_Scene");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            health--;
            ammo--;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Debug.Log($"Player Health & Ammo : {health} & {ammo}");
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            SavePlayerState(SceneManager.GetActiveScene().name);
        }
    }
    #endregion

    public void SavePlayerState(string currentScene)
    {
        if (playerInstance != null)
        {
            GameSaveData saveData = new GameSaveData() //��������
            {
                //������ ������ ����
                playerPos = playerInstance.transform.position,
                health = this.health,
                ammo = this.ammo,
                currentSceneName = currentScene
            };
            //������ ����
            SaveManager.SaveGame(saveData);
        }     
    }

    public void LoadPlayerState(GameSaveData loadedData)
    {
        //����� �����͸� ����� �÷��̾��� ü�°� ź�� ���� ������
        if (loadedData != null)
        {
            health = loadedData.health; 
            ammo = loadedData.ammo;

            //CreatePlayer(loadedData.playerPos); StageManager���� ȣ���
            //SceneManager.LoadScene(loadedData.currentSceneName); Continue���� ȣ���
            //�ڵ带 �ٿ��� �÷��̾��� ���¸� �����ϴ� ���Ҹ� ����ϰ� �ϱ�
        }
        else
        {
            Debug.Log("������ �����Ͱ� ����");
        }
    }

    public void TransitionToScene(string nextScene)
    {
        //���� �� �Ѿ�� �� �Լ� ȣ���ؼ� ����� �� �̵� ���ÿ� ó��
        SavePlayerState(SceneManager.GetActiveScene().name);
        LoadingSceneController.LoadScene(nextScene);
    }

    #region �Ⱦ���
    public void UpdatePlayerState(int newHealth, int newAmmo)
    {
        health = newHealth;
        ammo = newAmmo;
    }

    public GameObject GetPlayer()
    {
        return playerInstance;
    }
    #endregion
}
