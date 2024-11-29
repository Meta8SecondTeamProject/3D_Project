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

        print("AmmoBelt_Merchant_Frog / Interaction / Start");

        if (DataManager.Instance.money >= price && DataManager.Instance.isAmmoBelt == false)
        {
            DataManager.Instance.money -= price;
            DataManager.Instance.isAmmoBelt = true;
            UIManager.Instance.GameSceneTextUpdate();
            UIManager.Instance.ChangeInteractionText(str = null);
            return;
        }

        NotEnoughMoney();

        if (DataManager.Instance.isAmmoBelt)
        {
            UIManager.Instance.ChangeInteractionText(str = "You already have ammo belt, so Get The Fuck Out!!!");
        }
    }
}
