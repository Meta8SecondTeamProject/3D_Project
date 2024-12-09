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

	public AudioClip deathClip;
	public AudioClip idleClip;

	protected virtual void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	protected virtual void OnEnable()
	{
		GameManager.Instance.enemy[enemyNumber].Add(gameObject);
		if (isBat)
			batMoveMent = GetComponentInParent<FliesMovement>();

		StartCoroutine(PlayIdleClipCoroutine());
	}
	//NOTE : 까마귀 제외 모든 적 사운드 추가
	private IEnumerator PlayIdleClipCoroutine()
	{
		while (gameObject.activeSelf == true)
		{
			yield return new WaitForSeconds(idleClip.length + Random.Range(3f, 5f));
			if (isBat)
			{
				AudioManager.Instance.PlaySFX(idleClip, transform.position, this.transform, 0.1f);
			}
			else
			{
				AudioManager.Instance.PlaySFX(idleClip, transform.position, this.transform, 0.1f);
			}
		}
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
			if (isBoss == false)
			{
				GameManager.Instance.enemy[enemyNumber].Remove(gameObject);
				//NOTE : 적 Death 사운드 추가(까마귀 제외)
				AudioManager.Instance.PlaySFX(deathClip, transform.position, null, 0.1f);
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
				//NOTE : 중간보스 Death 사운드 추가
				AudioManager.Instance.PlaySFX(deathClip, transform.position, null, 0.1f);
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
			//NOTE : 사운드 추가됨
			case 0:
				DataManager.Instance.data.money++;
				DataManager.Instance.totalKillCount++;
				UIManager.Instance.GameSceneTextUpdate();
				break;
			case 1:
				DataManager.Instance.fishKillCount--;
				DataManager.Instance.totalKillCount++;
				break;
			case 2:
				DataManager.Instance.birdKillCount--;
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
