using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor_Merchant_Frog : NPC
{
    public AudioClip healClip;
    protected override void Start()
    {
        base.Start();
        price = 5;
        interactionValue = 1;
    }

    public override void Interaction()
    {
        base.Interaction();
        print("Doctor_Merchant_Frog / Interaction / Start");
        if (DataManager.Instance.data.money >= price && DataManager.Instance.data.HP < 2)
        {
            DataManager.Instance.data.money -= price;
            DataManager.Instance.data.HP += interactionValue;
            UIManager.Instance.GameSceneTextUpdate();
            UIManager.Instance.ChangeInteractionText(str = null);
            GameManager.Instance.player.bodyChange.BodyChange();
            AudioManager.Instance.PlaySFX(healClip);
            return;
        }

        NotEnoughMoney();

        if (DataManager.Instance.data.HP >= 2)
        {
            UIManager.Instance.ChangeInteractionText(str = "Your health is full!");
        }
    }
}
