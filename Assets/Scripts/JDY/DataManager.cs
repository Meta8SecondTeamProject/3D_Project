using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : SingletonManager<DataManager>
{
    public Data data;
    //스테이지마다 스폰될 플레이어의 위치 정보.
    public Vector3[] playerStartPos;
    public Vector3 currentStartPos;

    public int jumpCount;
    public int triggerOn;

    public float bombFliesSpeed;
    public float fishSpeed;
    public float birdSpeed;

    //최대 스폰될 수 있는 적의 수
    public int fliesMaxSpawnCount;
    public int fishMaxSpawnCount;
    public int birdMaxSpawnCount;
    public int birdBlackMaxSpawnCount;

    //현재 킬 수
    public int fliesKillCount;
    public int fishKillCount;
    public int birdKillCount;

    public int totalKillCount;

    public Vector3 StartPosition()
    {
        NewGamePositionSet();

        int index = SceneManager.GetActiveScene().buildIndex;
        if (playerStartPos.Length >= index - 2)
            currentStartPos = playerStartPos[index - 3];
        return currentStartPos;
    }

    protected override void Awake()
    {
        base.Awake();
        //NewGame();
        //SaveManager.SaveGame(data);

        //데이터를 처음부터 로드하여서 관련 정보를 받을 수 있게
        if (data != null)
        {
            data = SaveManager.LoadGame();
            //Debug.Log(data.isClear);
        }
        else
        {
            //Debug.Log("데이터 없음!");
        }

        //if (data == null)
        //{
        //    NewGame();

        //}
    }

    private void NewGamePositionSet()
    {

        if (DataManager.Instance.data.isClear == false)
        {
            playerStartPos[0] = new Vector3(27, 45, 5);
        }
        else
        {
            playerStartPos[0] = new Vector3(-359.5f, 18f, 365.5f);
        }
    }

    [ContextMenu("Test")]
    private void SaveTest()
    {
        SaveManager.SaveGame(data);
    }

    public void Save()
    {
        data.isClear = true;
        data.currentSceneName = SceneManager.GetActiveScene().name;
        SaveManager.SaveGame(data);
    }

    public void Load()
    {
        data = SaveManager.LoadGame();
    }

    #region 세이브 로드 테스트용 
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            data.ammo = 16;
            data.money++;
            UIManager.Instance.GameSceneTextUpdate();
            Debug.Log("탄약, 파리 증가됨");

        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("SaveGame호출");
            //SaveManager.SaveGame(data);
            Save();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            //data = SaveManager.LoadGame();
            Load();
            Debug.Log("LoadGame호출");
            UIManager.Instance.TransitionToLoadScene(data.currentSceneName);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Debug.Log("BBH씬으로 이동");
            UIManager.Instance.TransitionToLoadScene("BBH_Scene");
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            Debug.Log("JDY씬으로 이동");
            UIManager.Instance.TransitionToLoadScene("JDY_Scene");
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Debug.Log("KCY씬으로 이동");
            UIManager.Instance.TransitionToLoadScene("KCY_Scene");
        }
    }
    #endregion

    public void RetryGame()
    {
        data.HP = 2;
        data.ammo = 4;
        Save();
        Load();
    }

}

[Serializable] //이 클래스가 JSON으로 변활될 수 있도록 설정하는데 필요함
public class Data
{
    public string currentSceneName;
    public int HP { get { return currentHP; } set { currentHP = Mathf.Clamp(value, 0, 2); } }
    public int currentHP;
    public int ammo { get { return currentAmmo; } set { currentAmmo = Mathf.Clamp(value, 0, maxAmmo); } }
    public int currentAmmo;
    public int maxAmmo;

    public int money;

    public bool isHat;
    public bool isAmmoBelt;
    public bool isDoubleJump;

    public bool isClear;
    public bool isHardClear;

    public bool isKilledBossBird;
    public bool isKilledBossFish;
    //TODO : 보스Enemy를 잡았는지 체크해주는 변수나 로직 작성요망
}
