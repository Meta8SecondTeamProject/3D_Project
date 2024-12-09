using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Merchant_Frog : NPC
{
    public AudioClip getAmmoClip;
    protected override void Start()
    {
        base.Start();
        price = 2;
        interactionValue = 4;
    }
    public override void Interaction()
    {
        base.Interaction();
        print("Ammo_Merchant_Frog / Interaction / Start");
        if (DataManager.Instance.data.money >= price && DataManager.Instance.data.ammo < DataManager.Instance.data.maxAmmo)
        {
            //NOTE : 사운드 추가됨
            DataManager.Instance.data.money -= price;
            DataManager.Instance.data.ammo += interactionValue;
            AudioManager.Instance.PlaySFX(getAmmoClip);
            UIManager.Instance.GameSceneTextUpdate();
            UIManager.Instance.ChangeInteractionText(str = null);
            return;
        }
        if (DataManager.Instance.data.ammo >= DataManager.Instance.data.maxAmmo)
        {
            UIManager.Instance.ChangeInteractionText(str = "Already full ammo!");
        }
        NotEnoughMoney();
    }
}
