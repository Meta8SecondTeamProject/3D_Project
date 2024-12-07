using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Boss_Spawn : NPC
{
	private bool isBoos;
	public GameObject spawnPos;
	public GameObject birdSpawner;
	public GameObject fishSpawner;

	private void Awake()
	{
		birdSpawner.SetActive(false);
		fishSpawner.SetActive(false);
	}

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
			birdSpawner.SetActive(true);
			fishSpawner.SetActive(true);
			Instantiate(chatWindow).transform.position = spawnPos.transform.position;
			UIManager.Instance.ChangeInteractionText(str = "The King Is Comming...");
		}
	}

	private void OnDisable()
	{
		isBoos = false;
	}


}
