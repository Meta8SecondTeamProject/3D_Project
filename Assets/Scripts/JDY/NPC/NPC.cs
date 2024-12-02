using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //�÷����� �׼ǿ��� ���� �¿��� �ǰ�
    public GameObject ambassadorWindow;

    public bool isMessage = false;
    public bool isInteraction = false;
    protected int price;
    protected int interactionValue;

    protected string str;

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
