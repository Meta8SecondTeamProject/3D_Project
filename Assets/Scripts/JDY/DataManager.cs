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
        if (playerStartPos.Length >= index - 3)
            currentStartPos = playerStartPos[index - 3];
        return currentStartPos;
    }

    private void Start()
    {
        //데이터를 처음부터 로드하여서 관련 정보를 받을 수 있게
        //data = SaveManager.LoadGame();
    }
}

[Serializable] //이 클래스가 JSON으로 변활될 수 있도록 설정하는데 필요함
public class Data
{
    public string currentSceneName;
    public int HP { get { return currentHP; } set { currentHP = Mathf.Clamp(value, 0, 2); } }
    private int currentHP;
    public int ammo { get { return currentAmmo; } set { currentAmmo = Mathf.Clamp(value, 0, maxAmmo); } }
    private int currentAmmo;
    public int maxAmmo;

    public int money;

    public bool isHat;
    public bool isAmmoBelt;
    public bool isDoubleJump;

    public bool isClear;
    public bool isHardClear;
}
