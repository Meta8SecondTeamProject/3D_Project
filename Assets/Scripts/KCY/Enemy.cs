using DinoFracture;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public abstract class Enemy : MonoBehaviour
{
	protected Rigidbody rb;
	protected bool isFly;
	protected float moveSpeed;
	protected Transform target;
	protected Vector3 moveDir;
	public Transform attackSpot;
	public GameObject fracture;
	public int enemyNumber;
	protected virtual void OnEnable()
	{
		GameManager.Instance.enemy[enemyNumber].Add(gameObject);
	}

	protected virtual void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	protected virtual void Start()
	{
		if (GameManager.Instance.player != null)
			target = GameManager.Instance.player.transform;

		if (isFly == false) { rb.useGravity = true; }
		else { rb.useGravity = false; }
	}


	protected virtual void Update()
	{
		if (target == null)
		{
			if (GameManager.Instance.player != null)
				target = GameManager.Instance.player.transform;

			return;
		}
		else
		{
			moveDir = target.position - attackSpot.transform.position;
		}
	}

	protected virtual void Move(Vector3 dir)
	{
		if (Time.timeScale == 0) return;
		rb.AddForce(dir * moveSpeed);
	}

	protected virtual void Look(Vector3 dir, float rotVal)
	{
		//Debug.Log($"Look TimeScale : {Time.timeScale}");
		if (Time.timeScale == 0) return;
		Quaternion dirRot = Quaternion.LookRotation(dir);
		rb.rotation = Quaternion.Slerp(rb.rotation, dirRot, rotVal * Time.deltaTime);
	}


	protected virtual void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Projectile"))
		{
			switch (enemyNumber)
			{
				case 0:
					DataManager.Instance.fliesKillCount++;
					break;
				case 1:
					DataManager.Instance.fishKillCount++;
					break;
				case 2:
					DataManager.Instance.birdKillCount++;
					break;
				case 3:
					DataManager.Instance.fliesKillCount++;
					break;
			}
			GameObject fracture = GameManager.Instance.pool.Pop(this.fracture.name);
			fracture.transform.position = transform.position;
			GameManager.Instance.enemy[enemyNumber].Remove(gameObject);
			Destroy(gameObject);
		}
	}

}
