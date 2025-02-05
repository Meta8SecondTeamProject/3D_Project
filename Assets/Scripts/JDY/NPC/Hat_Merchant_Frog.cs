using UnityEngine;

public class Hat_Merchant_Frog : Interaction
{
    [SerializeField]
    private GameObject hat;

    protected override void Start()
    {
        base.Start();
        price = 20;
        hat.SetActive(!DataManager.Instance.data.isHat);
    }

    public override void InteractionEvent()
    {
        base.InteractionEvent();

        if (isStop)
        {
            isStop = false;
            return;
        }


        if (DataManager.Instance.data.isHat)
        {
            UIManager.Instance.ChangeInteractionText(str = "You already have a hat!");
        }
        else
        {
            DataManager.Instance.data.money -= price;
            DataManager.Instance.data.isHat = true;
            UIManager.Instance.GameSceneTextUpdate();
            UIManager.Instance.ChangeInteractionText(str = null);
            GameManager.Instance.player.bodyChange.BodyChange();
            hat.SetActive(false);
            GameManager.Instance.IsHat();
        }
    }
}
