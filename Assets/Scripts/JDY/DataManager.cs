using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : SingletonManager<DataManager>
{
    public Data data;
    //������������ ������ �÷��̾��� ��ġ ����.
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
        //�����͸� ó������ �ε��Ͽ��� ���� ������ ���� �� �ְ�
        //data = SaveManager.LoadGame();
    }
}

[Serializable] //�� Ŭ������ JSON���� ��Ȱ�� �� �ֵ��� �����ϴµ� �ʿ���
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
