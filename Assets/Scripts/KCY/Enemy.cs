using DinoFracture;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
	private FliesMovement batMoveMent;

	protected Rigidbody rb;
	protected Transform target;
	protected Vector3 moveDir;
	public float moveSpeed;
	protected bool isFly;

	public Transform attackSpot;
	public GameObject fracture;
	public GameObject shotgunShell;

	public int enemyNumber;
	public bool isBat;

	public bool isBoss;
	public bool isBossFish;
	public bool isBossBird;
	public float bossHp;
	public float bossMaxHp;
	public float hpAmount { get { return bossHp / bossMaxHp; } }
	public Image hpBar;

	protected virtual void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	protected virtual void OnEnable()
	{
		GameManager.Instance.enemy[enemyNumber].Add(gameObject);
		if (isBat)
			batMoveMent = GetComponentInParent<FliesMovement>();
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
			Debug.Log("적 총알 감지");
			if (isBoss == false)
			{
				Debug.Log("ㅎㅇ");
				GameManager.Instance.enemy[enemyNumber].Remove(gameObject);
				KillCountUpdater();
				FractureGen();
				if (DataManager.Instance.totalKillCount % 3 == 0 && isBoss == false)
				{
					AmmoGen(collision);
				}
				if (isBat == false)
				{
					Destroy(gameObject);
				}
				else if (isBat)
				{
					Destroy(batMoveMent.gameObject);
				}
			}
			else
			{
				Debug.Log("나 보스야");
				Boss();
				if (DataManager.Instance.data.isKilledBossBird || DataManager.Instance.data.isKilledBossFish)
				{
					for (int i = 0; i < 4; i++)
					{
						AmmoGen(collision);
					}
				}
			}
		}
	}

	private void Boss()
	{
		bossHp--;
		hpBar.fillAmount = hpAmount;
		Debug.Log("아야");
		if (bossHp <= 0)
		{
			if (isBossBird)
			{

				DataManager.Instance.data.isKilledBossBird = true;
			}
			else if (isBossFish)
			{
				DataManager.Instance.data.isKilledBossFish = true;
			}
			FractureGen();
			Destroy(gameObject);
		}


	}

	private void AmmoGen(Collision collision)
	{
		Debug.Log($"총알 생성됨 {shotgunShell.name}");
		GameObject shell = GameManager.Instance.pool.Pop(shotgunShell.name);
		shell.transform.position = collision.gameObject.transform.position;
	}

	private void KillCountUpdater()
	{
		switch (enemyNumber)
		{
			case 0:
				DataManager.Instance.data.money++;
				DataManager.Instance.totalKillCount++;
				UIManager.Instance.GameSceneTextUpdate();
				break;
			case 1:
				DataManager.Instance.fishKillCount++;
				DataManager.Instance.totalKillCount++;
				break;
			case 2:
				DataManager.Instance.birdKillCount++;
				DataManager.Instance.totalKillCount++;
				break;
			case 3:
				DataManager.Instance.data.money++;
				DataManager.Instance.totalKillCount++;
				UIManager.Instance.GameSceneTextUpdate();
				break;
			default:
				break;
		}
	}
	private void FractureGen()
	{
		GameObject fracture = GameManager.Instance.pool.Pop(this.fracture.name);
		fracture.transform.position = transform.position;
	}
}
