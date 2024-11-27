using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_God_Frog_Test : NPC_Test
{
	public override void Interaction()
	{
		if (gameManager.money >= 2 && gameManager.ammo < gameManager.maxAmmon)
		{
			gameManager.ammo = Mathf.Max(gameManager.ammo + 4, gameManager.maxAmmon);
		}
	}
}
