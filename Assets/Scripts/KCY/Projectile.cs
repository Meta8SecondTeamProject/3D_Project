using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public GameObject explosion;
	public ParticleSystem m_particle;

	private Rigidbody rb;
	private SphereCollider m_coll;
	public LayerMask includeLayer;
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		m_coll = GetComponent<SphereCollider>();
	}

	private void OnEnable()
	{
		m_particle.Play();
		explosion.SetActive(false);
	}
	private void OnDisable()
	{
		explosion.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			Debug.Log("트리거 적 감지");
			rb.velocity = Vector3.zero;
			m_particle.Stop(true);
			explosion.SetActive(true);
			StartCoroutine(GameManager.Instance.pool.Push(this.gameObject, 1.5f));
		}
		else
		{
			Debug.Log($"? : {other.gameObject.name}");
			rb.velocity = Vector3.zero;
			m_particle.Stop(true);
			explosion.SetActive(true);
			StartCoroutine(GameManager.Instance.pool.Push(this.gameObject, 1.5f));
		}
	}
}
