using Unity.VisualScripting;
using UnityEngine;

public class Water : MonoBehaviour
{
	[Header("�η�, ����")]
	public float buoyancyForce = 30f;
	public float waterDrag = 5f;

	private float defaultBuoyancy;

	public AudioClip contactWaterClip;


	private void Start()
	{
		defaultBuoyancy = buoyancyForce;

		//Start ���� �� ���ӸŴ����� ��ϵ� �÷��̾��� ������ٵ�� Frog_Move ��ũ��Ʈ ������

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
		//���̾�� ���ϴ� �ɷ� �����غý��ϴ�.
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

