using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{

	internal Player player;

	protected override void Awake()
	{
		base.Awake();
		player = FindAnyObjectByType<Player>();
	}

	private void Start()
	{

	}


}
