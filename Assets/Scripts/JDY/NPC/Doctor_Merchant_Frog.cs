using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor_Merchant_Frog : NPC
{
	private void Start()
	{
		price = 5;
		interactionValue = 1;
	}

	public override void Interaction()
	{
		base.Interaction();
		print("Doctor_Merchant_Frog / Interaction / Start");
		if (DataManager.Instance.money >= price && DataManager.Instance.health < 2)
		{
			DataManager.Instance.money -= price;
			DataManager.Instance.health += interactionValue;
		}
	}
}
