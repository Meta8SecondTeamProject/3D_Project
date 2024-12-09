using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Shell : MonoBehaviour
{
	private Rigidbody rb;
	public bool isEnemyDrop = false;
	public float moveSpeed;
	public ObjectPool pool;
	public GameObject shell;

	public AudioClip getAmmoClip;
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void OnEnable()
	{
		rb.velocity = Vector3.zero;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			//NOTE : 사운드 추가됨
			AudioManager.Instance.PlaySFX(getAmmoClip);
			DataManager.Instance.data.ammo += 4;
			UIManager.Instance.GameSceneTextUpdate();
			if (isEnemyDrop)
			{
				GameManager.Instance.pool.Push(shell);
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
	}

	public void MoveToPlayer()
	{
		Vector3 angle = GameManager.Instance.player.transform.position - transform.position;
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