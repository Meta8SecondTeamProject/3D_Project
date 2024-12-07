using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour/*, ISellable*/
{
	//�÷����� �׼ǿ��� ���� �¿��� �ǰ�
	[Tooltip("���ε��� ���â, �����ʸ� ������")]
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
	//		// ��������, ��� �Ȱ���
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
