using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Boss_Spawn : NPC
{
	private bool isBoos;
	public GameObject spawnPos;

	protected override void Start()
	{

	}

	private void OnEnable()
	{
		isBoos = true;
	}

	public override void Interaction()
	{
		base.Interaction();

		if (isBoos)
		{
			isBoos = false;
			Instantiate(chatWindow).transform.position = spawnPos.transform.position;
			UIManager.Instance.ChangeInteractionText(str = "Are You Ready?");
		}
	}

	private void OnDisable()
	{
		isBoos = false;
	}


}
