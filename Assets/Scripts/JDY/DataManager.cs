using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonManager<DataManager>
{
    public int health;
    public const int maxHealth = 2;
    private int _ammo;
    public int ammo { get { return _ammo; } set { _ammo = Mathf.Clamp(value, 0, maxAmmo); } }
    public int maxAmmo;
    public int money;
    public int jumpCount;
    public int maxJumpCount;

    public bool isHat;
    public bool isAmmoBelt;
    public bool isDoubleJump;
}
