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

        if (DataManager.Instance.data.money >= price && DataManager.Instance.data.isHat == false)
        {
            DataManager.Instance.data.money -= price;
            DataManager.Instance.data.isHat = true;
            UIManager.Instance.GameSceneTextUpdate();
            UIManager.Instance.ChangeInteractionText(str = null);
            return;
        }

        NotEnoughMoney();

        if (DataManager.Instance.data.isHat)
        {
            UIManager.Instance.ChangeInteractionText(str = "You already have a hat!");
        }
    }
}
