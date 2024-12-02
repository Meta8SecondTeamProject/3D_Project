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
            Instance = this; //싱글톤 초기화
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public void CreatePlayer(Vector3 pos)
    { 
        #region 플레이어 생성 방지 로직
        string currentScene = SceneManager.GetActiveScene().name;

        //GameStartScene에서는 플레이어 생성하지 않음, 사실 호출할 예정도 없음
        if (currentScene == "GameStartScene")
        {
            Debug.Log("Player creation skipped in GameStartScene");
            return;
        }
        #endregion

        if (playerInstance == null)
        {
            //플레이어 프리팹을 이용해 플레이어를 생성하고,
            //플레이어의 위치를 이동시켜줌
            playerInstance = Instantiate(playerPrefab);
            playerInstance.transform.position = pos;
            Debug.Log($"플레이어를 생성하고 이동시킴 {pos}");
        }
        else
        {
            playerInstance.transform.position = pos;
            Debug.Log($"플레이어를 이동시킴 {pos}");
        }
    }

    #region 테스트용
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
            GameSaveData saveData = new GameSaveData() //생성자임
            {
                //저쟝용 데이터 생성
                playerPos = playerInstance.transform.position,
                health = this.health,
                ammo = this.ammo,
                currentSceneName = currentScene
            };
            //데이터 저장
            SaveManager.SaveGame(saveData);
        }     
    }

    public void LoadPlayerState(GameSaveData loadedData)
    {
        //저장된 데이터를 사용해 플레이어의 체력과 탄약 등을 복원함
        if (loadedData != null)
        {
            health = loadedData.health; 
            ammo = loadedData.ammo;

            //CreatePlayer(loadedData.playerPos); StageManager에서 호출됨
            //SceneManager.LoadScene(loadedData.currentSceneName); Continue에서 호출됨
            //코드를 줄여서 플레이어의 상태를 복원하는 역할만 담당하게 하기
        }
        else
        {
            Debug.Log("저장한 데이터가 없음");
        }
    }

    public void TransitionToScene(string nextScene)
    {
        //추후 씬 넘어갈때 이 함수 호출해서 저장과 씬 이동 동시에 처리
        SavePlayerState(SceneManager.GetActiveScene().name);
        LoadingSceneController.LoadScene(nextScene);
    }

    #region 안쓰임
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
