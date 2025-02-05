using UnityEngine;

public class Doctor_Merchant_Frog : Interaction
{
    public AudioClip healClip;

    protected override void Start()
    {
        base.Start();
        price = 5;
        interactionValue = 1;
    }

    public override void InteractionEvent()
    {
        base.InteractionEvent();
        if (isStop)
        {
            isStop = false;
            return;
        }

        if (DataManager.Instance.data.HP < 2)
        {
            DataManager.Instance.data.money -= price;
            DataManager.Instance.data.HP += interactionValue;
            UIManager.Instance.GameSceneTextUpdate();
            UIManager.Instance.ChangeInteractionText(str = null);
            GameManager.Instance.player.bodyChange.BodyChange();
            AudioManager.Instance.PlaySFX(healClip);
        }
        else
        {
            UIManager.Instance.ChangeInteractionText(str = "Your health is full!");
        }
    }
}
