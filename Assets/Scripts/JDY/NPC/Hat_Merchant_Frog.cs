using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat_Merchant_Frog : NPC
{
    private void Start()
    {
        price = 20;
    }

    public override void Interaction()
    {
        base.Interaction();

        print("Hat_Merchant_Frog / Interaction / Start");

        if (DataManager.Instance.money >= price && DataManager.Instance.isHat == false)
        {
            DataManager.Instance.money -= price;
            DataManager.Instance.isHat = true;
        }
    }

}
