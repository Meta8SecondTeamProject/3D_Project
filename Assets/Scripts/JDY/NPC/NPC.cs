using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //Åº¶ì, ¸ðÀÚ, Ã¼·Â, Åº¾à

    public bool isMessage = false;
    public bool isInteraction = false;
    protected int price;
    protected int interactionValue;

    protected string str;
    //money<price
    //Ã¼·ÂÀÌ³ª ºÒ¸´ÀÌ ²ËÂ÷ ÀÖÀ» ‹š

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
