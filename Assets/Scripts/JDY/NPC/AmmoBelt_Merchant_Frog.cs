using UnityEngine;

public class AmmoBelt_Merchant_Frog : NPC
{
    [SerializeField]
    private GameObject belt;
    protected override void Start()
    {
        base.Start();
        price = 20;
        belt.SetActive(!DataManager.Instance.data.isAmmoBelt);
    }

    public override void Interaction()
    {
        base.Interaction();

        print("AmmoBelt_Merchant_Frog / Interaction / Start");

        if (DataManager.Instance.data.money >= price && DataManager.Instance.data.isAmmoBelt == false)
        {
            DataManager.Instance.data.money -= price;
            DataManager.Instance.data.isAmmoBelt = true;
            UIManager.Instance.GameSceneTextUpdate();
            UIManager.Instance.ChangeInteractionText(str = null);
            GameManager.Instance.player.bodyChange.BodyChange();
            belt.SetActive(!DataManager.Instance.data.isAmmoBelt);
            DataManager.Instance.data.maxAmmo = 32;
            return;
        }

        NotEnoughMoney();

        if (DataManager.Instance.data.isAmmoBelt)
        {
            UIManager.Instance.ChangeInteractionText(str = "You already have ammo belt");
        }
    }
}
