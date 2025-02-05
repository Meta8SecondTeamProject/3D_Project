using UnityEngine;

public class Wizard_Merchant_Frog : Interaction
{
    public AudioClip clip;

    protected override void Start()
    {
        base.Start();
        price = 20;
    }

    public override void InteractionEvent()
    {
        base.InteractionEvent();

        if (isStop)
        {
            isStop = false;
            return;
        }

        if (DataManager.Instance.data.isDoubleJump == false)
        {
            DataManager.Instance.data.money -= price;
            DataManager.Instance.data.isDoubleJump = true;
            DataManager.Instance.jumpCount = 2;
            UIManager.Instance.GameSceneTextUpdate();
            UIManager.Instance.ChangeInteractionText(str = null);
            AudioManager.Instance.PlaySFX(clip);
        }
        else
        {
            UIManager.Instance.ChangeInteractionText(str = "You are already enchanted!");
        }
    }
}
