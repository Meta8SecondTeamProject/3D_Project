using UnityEngine;

public class Interaction : MonoBehaviour
{
    [Tooltip("상인들은 대사창, 스포너면 프리팹")]
    public GameObject interactionEffectObject;

    public bool isMessage = false;
    public bool isInteraction = false;
    public bool isNonInteractive = false;

    protected bool isStop = false;
    protected int price;
    protected int interactionValue;
    protected string str;


    protected virtual void Start()
    {
        interactionEffectObject.SetActive(false);
    }

    protected bool NotEnoughMoney()
    {
        if (DataManager.Instance.data.money < price)
        {
            UIManager.Instance.ChangeInteractionText(str = "Not Enough Money");
            return true;
        }
        return false;
    }

    /// <summary>
    /// 플레이어와의 상호작용
    /// </summary>
    public virtual void InteractionEvent()
    {
        if (isInteraction == false)
        {
            isStop = true;
            return;
        }

        isStop = NotEnoughMoney();
    }
}
