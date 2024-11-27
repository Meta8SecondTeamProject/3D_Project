using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{

    internal Player player;
    internal List<Enemy> flies = new List<Enemy>();
    public int money = 0;
    public int ammo = 4;
    public int maxAmmon = 16;

    protected override void Awake()
    {
        base.Awake();
        player = FindAnyObjectByType<Player>();
    }

    private void Start()
    {

    }


}
