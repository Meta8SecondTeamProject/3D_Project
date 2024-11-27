using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonManager<DataManager>
{
    public int health;
    private const int maxHealth = 2;
    public int ammo;
    public int maxAmmo;
    public int money;
    public int jumpCount;
    private int maxJumpCount;

    public bool isHat;
    public bool isAmmoBelt;
}
