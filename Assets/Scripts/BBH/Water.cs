using Unity.VisualScripting;
using UnityEngine;

public class Water : MonoBehaviour
{
	[Header("부력, 저항")]
	public float buoyancyForce = 30f;
	public float waterDrag = 5f;

	private float defaultBuoyancy;

	public AudioClip contactWaterClip;


	private void Start()
	{
		defaultBuoyancy = buoyancyForce;

		//Start 됐을 때 게임매니저에 등록된 플레이어의 리지드바디와 Frog_Move 스크립트 가져옴

	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			AudioManager.Instance.PlaySFX(contactWaterClip, GameManager.Instance.player.transform.position);
		}

	}

	private void OnTriggerStay(Collider other)
	{
		//레이어로 비교하는 걸로 변경해봤습니다.
		if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Item"))
		{
			if (other.TryGetComponent(out Rigidbody rb))
			{
				Vector3 force = Vector3.up * buoyancyForce;
				rb.AddForce(force, ForceMode.Acceleration);
				rb.velocity *= 1f - (waterDrag * Time.deltaTime);
				Debug.Log("Stay");
				buoyancyForce += 0.2f;

			}
			if (other.TryGetComponent(out Frog_Move frog_Move))
			{
				frog_Move.isWater = true;
			}
		}

	}


	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			if (other.TryGetComponent(out Frog_Move frog_Move))
				frog_Move.isWater = false;
			buoyancyForce = defaultBuoyancy;
		}
	}
}

