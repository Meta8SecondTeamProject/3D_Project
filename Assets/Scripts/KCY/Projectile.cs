using System.Collections;
using System.Collections.Generic;
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
	}
	private void OnDisable()
	{
		explosion.SetActive(false);
	}

	private void FixedUpdate()
	{
		DetectCollision();


	}
	private void DetectCollision()
	{
		Collider[] colls = Physics.OverlapSphere(gameObject.transform.position, m_coll.radius, includeLayer);
		foreach (Collider coll in colls)
		{
			rb.velocity = Vector3.zero;
			m_particle.Stop(true);
			explosion.SetActive(true);
			StartCoroutine(GameManager.Instance.pool.Push(this.gameObject, 1.5f));
		}
	}
}
