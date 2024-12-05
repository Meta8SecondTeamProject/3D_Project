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
		StartCoroutine(GameManager.Instance.pool.Push(this.gameObject, 5f));
		explosion.SetActive(false);
		m_particle.Play();
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

		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.layer != LayerMask.NameToLayer("Player") && collision.collider.gameObject.layer != LayerMask.NameToLayer("Projectile"))
		{
			Debug.Log($"������ : {collision.collider.gameObject.name}");
			Debug.Log("�ߵ�");
			rb.velocity = Vector3.zero;
			m_particle.Stop(true);
			explosion.SetActive(true);
		}
	}

}
