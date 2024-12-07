using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ChatWindow : MonoBehaviour
{
	//임시
	//플레이어의 위치를 받을수 있는 싱글톤이 있을 시 해당 객체를 참조
	private Player player;
	private void Start()
	{
		player = GameManager.Instance?.player;
	}
	private void Update()
	{
		WindowLookCam();
	}

	private void WindowLookCam()
	{
		if (player == null)
		{
			player = GameManager.Instance?.player;
			return;
		}
		gameObject.transform.LookAt(player.frogLook.mainCamera.transform.position);
	}
}
