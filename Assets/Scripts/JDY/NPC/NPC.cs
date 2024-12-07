using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour/*, ISellable*/
{
	//플레이의 액션에서 같이 온오프 되게
	[Tooltip("상인들은 대사창, 스포너면 프리팹")]
	public GameObject chatWindow;

	public bool isMessage = false;
	public bool isInteraction = false;
	protected int price;
	protected int interactionValue;

	protected string str;


	protected virtual void Start()
	{
		chatWindow.SetActive(false);
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

	//public virtual void OnTriggerStay(Collider findPlayer)
	//{
	//	if (findPlayer.transform.TryGetComponent<IBuiable>(out IBuiable bu))
	//	{
	//		//this.SellSometing();
	//		// 누구한테, 어떤걸 팔건지
	//		//bu.BuySometing();
	//	}
	//}

	//	public int SellSometing()
	//	{
	//		return price;
	//	}
	//	public int SellPrice { get => price; set { price = value; } }

	//	int ISellable.SellSometing(IBuiable customer)
	//	{
	//		throw new System.NotImplementedException();
	//	}
}
