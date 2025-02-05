using UnityEngine;

public class Ammo_Merchant_Frog : Interaction
{
    public AudioClip getAmmoClip;

    protected override void Start()
    {
        base.Start();
        price = 2;
        interactionValue = 4;
    }

    public override void InteractionEvent()
    {
        base.InteractionEvent();
        if (isStop)
        {
            isStop = false;
            return;
        }

        if (DataManager.Instance.data.ammo < DataManager.Instance.data.maxAmmo)
        {
            DataManager.Instance.data.money -= price;
            DataManager.Instance.data.ammo += interactionValue;
            AudioManager.Instance.PlaySFX(getAmmoClip);
            UIManager.Instance.GameSceneTextUpdate();
            UIManager.Instance.ChangeInteractionText(str = null);
        }
        else
        {
            UIManager.Instance.ChangeInteractionText(str = "Already full ammo!");
        }
    }
}
