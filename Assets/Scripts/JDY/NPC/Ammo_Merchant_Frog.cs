using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Merchant_Frog : NPC
{
	private void Start()
	{
		price = 2;
		interactionValue = 4;
	}
	public override void Interaction()
	{
		base.Interaction();
		print("Ammo_Merchant_Frog / Interaction / Start");
		if (DataManager.Instance.money >= price && DataManager.Instance.ammo < DataManager.Instance.maxAmmo)
		{
			DataManager.Instance.money -= price;
			DataManager.Instance.ammo += interactionValue;
		}
	}
}
