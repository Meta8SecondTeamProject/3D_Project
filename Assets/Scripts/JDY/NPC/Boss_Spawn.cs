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

	//NOTE : 만약 보스 전용 BGM이 선정되면 여기서 변경하는걸로?

	private void Awake()
	{
		birdSpawner.SetActive(false);
		fishSpawner.SetActive(false);
	}

	protected override void Start()
	{
		//상속받은 Start 아무동작안하도록 초기화.
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
