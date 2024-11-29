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
            UIManager.Instance.GameSceneTextUpdate();
            UIManager.Instance.ChangeInteractionText(str = null);
            return;
        }

        NotEnoughMoney();

        if (DataManager.Instance.isHat)
        {
            UIManager.Instance.ChangeInteractionText(str = "You already have a hat!");
        }
    }
}
