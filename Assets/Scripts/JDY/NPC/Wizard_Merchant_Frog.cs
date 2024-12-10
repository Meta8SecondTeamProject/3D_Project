using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Merchant_Frog : NPC
{
	public AudioClip clip;
	protected override void Start()
	{
		base.Start();
		price = 20;
	}

	public override void Interaction()
	{
		base.Interaction();

		print("Wizard_Merchant_Frog / Interaction / Start");

		if (DataManager.Instance.data.money >= price && DataManager.Instance.data.isDoubleJump == false)
		{
			DataManager.Instance.data.money -= price;
			DataManager.Instance.data.isDoubleJump = true;
			DataManager.Instance.jumpCount = 2;
			UIManager.Instance.GameSceneTextUpdate();
			UIManager.Instance.ChangeInteractionText(str = null);
			AudioManager.Instance.PlaySFX(clip);
			return;
		}

		NotEnoughMoney();

		if (DataManager.Instance.data.isDoubleJump)
		{
			UIManager.Instance.ChangeInteractionText(str = "You are already enchanted!");
		}
	}
}
