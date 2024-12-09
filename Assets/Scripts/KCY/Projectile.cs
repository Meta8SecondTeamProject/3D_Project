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

	public AudioClip hitClip;
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		m_coll = GetComponent<SphereCollider>();
	}

	private void OnEnable()
	{
		m_particle.Play();
		explosion.SetActive(false);
		m_coll.enabled = true;
		StartCoroutine(GameManager.Instance.pool.Push(this.gameObject, 1.5f));
	}
	private void OnDisable()
	{
		explosion.SetActive(false);
	}

	private void OnCollisionEnter(Collision other)
	{
        //AudioManager.Instance.PlaySFX(hitClip, other.transform.position);

        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			Debug.Log("트리거 적 감지");
			rb.velocity = Vector3.zero;
			m_particle.Stop(true);
			explosion.SetActive(true);
			m_coll.enabled = false;
		}
		else
		{
			Debug.Log($"? : {other.gameObject.name}");
			rb.velocity = Vector3.zero;
			m_particle.Stop(true);
			explosion.SetActive(true);
		}
	}
}
