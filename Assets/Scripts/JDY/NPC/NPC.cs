using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //플레이의 액션에서 같이 온오프 되게
    [Tooltip("상인들은 대사창, 스포너면 프리팹")]
    public GameObject ambassadorWindow;

    public bool isMessage = false;
    public bool isInteraction = false;
    protected int price;
    protected int interactionValue;

    protected string str;

    protected virtual void Start()
    {
        ambassadorWindow.SetActive(false);
    }
    protected void NotEnoughMoney()
    {
        if (DataManager.Instance.data.money < price)
        {
            UIManager.Instance.ChangeInteractionText(str = "Not Enough Money");
            return;
        }
    }

    public virtual void Interaction()
    {
        if (isInteraction == false)
        {
            return;
        }
    }
}
