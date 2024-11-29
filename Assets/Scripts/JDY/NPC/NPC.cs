using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //ź��, ����, ü��, ź��

    public bool isMessage = false;
    public bool isInteraction = false;
    protected int price;
    protected int interactionValue;

    protected string str;
    //money<price
    //ü���̳� �Ҹ��� ���� ���� ��

    protected void NotEnoughMoney()
    {
        if (DataManager.Instance.money < price)
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
