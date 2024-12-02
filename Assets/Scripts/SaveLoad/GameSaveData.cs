using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //�� Ŭ������ JSON���� ��Ȱ�� �� �ֵ��� �����ϴµ� �ʿ���
public class GameSaveData
{
    public Vector3 playerPos;
    public int health;
    public int ammo;
    public string currentSceneName;
}
