using UnityEngine;

public class AmmoBelt_Merchant_Frog : Interaction
{
    [SerializeField]
    private GameObject belt;
    public AudioClip clip;

    protected override void Start()
    {
        base.Start();
        price = 20;
        belt.SetActive(!DataManager.Instance.data.isAmmoBelt);
    }

    public override void InteractionEvent()
    {
        base.InteractionEvent();

        if (isStop)
        {
            isStop = false;
            return;
        }

        if (DataManager.Instance.data.isAmmoBelt)
        {
            UIManager.Instance.ChangeInteractionText(str = "You already have ammo belt");
        }
        else
        {
            DataManager.Instance.data.money -= price;
            DataManager.Instance.data.isAmmoBelt = true;
            UIManager.Instance.GameSceneTextUpdate();
            UIManager.Instance.ChangeInteractionText(str = null);
            GameManager.Instance.player.bodyChange.BodyChange();
            belt.SetActive(false);
            DataManager.Instance.data.maxAmmo = 32;
            AudioManager.Instance.PlaySFX(clip);
        }
    }
}
