using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor_Frog_Test : NPC_Test
{
	public override void Interaction()
	{
		print("닥터 개구리와 상호작용!");
		if (gameManager.money >= 5 /*&& gameManager.player.health*/)
		{
			//gameManager.player.health += 1;
			gameManager.money -= 5;
		}
	}
}
