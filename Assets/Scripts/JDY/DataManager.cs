using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
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

    public int fliesMaxCount;
    public int fishMaxCount;
    public int birdMaxCount;
    public int birdBlackMaxCount;

    public int fliesKillCount;
    public int fishKillCount;
    public int birdKillCount;

    public Vector3 StartPosition()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(index);
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
            Debug.Log(data.isClear);
        }
        else
        {
            Debug.Log("데이터 없음!");
        }

        //if (data == null)
        //{
        //    NewGame();
            
        //}

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
    //private void Update()
    //{
    //   
    //    if (Input.GetKeyDown(KeyCode.Keypad1))
    //    {
    //        data.ammo--;
    //        Debug.Log("탄약 감소됨");

    //    }
    //    if (Input.GetKeyDown(KeyCode.Keypad2))
    //    {
    //        SaveManager.SaveGame(data);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Keypad3))
    //    {
    //        UIManager.Instance.StartCoroutine(UIManager.Instance.Loading("KCY_Scene"));
    //    }
    //    if (Input.GetKeyDown(KeyCode.Keypad4))
    //    {
    //        Debug.Log($"DataManager.data.ammo : {data.ammo}");
    //        Debug.Log($"DataManager.data.ammo : {data.currentAmmo}");
    //    }
    //    if (Input.GetKeyDown(KeyCode.Keypad5))
    //    {
    //        data = SaveManager.LoadGame();
    //        Debug.Log("LoadGame호출");
    //        UIManager.Instance.StartCoroutine(UIManager.Instance.Loading(data.currentSceneName));

    //    }
    //    if  (Input.GetKeyDown(KeyCode.Keypad6))
    //    {
    //        UIManager.Instance.StartCoroutine(UIManager.Instance.Loading("JDY_Scene"));
    //    }
    //    
    //}
    #endregion

    public void RetryGame()
    {
        data.HP = 2;
        data.ammo = 4;
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
}
