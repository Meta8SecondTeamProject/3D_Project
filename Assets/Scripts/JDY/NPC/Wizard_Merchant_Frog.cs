using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Merchant_Frog : NPC
{

    private void Start()
    {
        price = 20;
    }

    public override void Interaction()
    {
        base.Interaction();

        print("Wizard_Merchant_Frog / Interaction / Start");
        if (DataManager.Instance.money >= price && DataManager.Instance.isDoubleJump == false)
        {
            DataManager.Instance.money -= price;
            DataManager.Instance.isDoubleJump = true;
        }
    }
}
