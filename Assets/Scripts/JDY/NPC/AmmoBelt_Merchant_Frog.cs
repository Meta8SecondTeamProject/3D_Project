using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBelt_Merchant_Frog : NPC
{
    private void Start()
    {
        price = 20;
    }

    public override void Interaction()
    {
        base.Interaction();

        print("Ammo_Merchant_Frog / Interaction / Start");

        if (DataManager.Instance.money >= price && DataManager.Instance.isAmmoBelt == false)
        {
            DataManager.Instance.money -= price;
            DataManager.Instance.isAmmoBelt = true;
        }
    }
}
