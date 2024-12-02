using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //이 클래스가 JSON으로 변활될 수 있도록 설정하는데 필요함
public class GameSaveData
{
    public Vector3 playerPos;
    public int health;
    public int ammo;
    public string currentSceneName;
}
