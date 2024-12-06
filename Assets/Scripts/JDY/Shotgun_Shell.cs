using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Shell : MonoBehaviour
{
	private Player player;
	private Rigidbody rb;
	public bool isEnemyDrop = false;
	public float moveSpeed;
	public ObjectPool pool;
	public GameObject shell;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		player = GameManager.Instance.player;
		pool = GameManager.Instance.pool;
	}

	private void OnEnable()
	{
		rb.velocity = Vector3.zero;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			DataManager.Instance.data.ammo += 4;
			UIManager.Instance.GameSceneTextUpdate();
			if (isEnemyDrop)
			{
				pool.Push(shell);
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
	}

	public void MoveToPlayer()
	{
		Vector3 angle = player.transform.position - transform.position;
		Quaternion rot = Quaternion.Euler(0, 0, 30);
		rb.velocity += angle.normalized * moveSpeed;
		rb.rotation *= rot;
	}

	private void FixedUpdate()
	{
		if (isEnemyDrop)
		{
			MoveToPlayer();
		}
	}
}