using UnityEngine;

public class Water : MonoBehaviour
{
	[Header("�η�, ����")]
	public float buoyancyForce = 30f;
	public float waterDrag = 5f;

	private float defaultBuoyancy;

	public AudioClip contactWaterClip;
	private AudioSource swimming;


	private void Start()
	{
		defaultBuoyancy = buoyancyForce;
		swimming = GetComponent<AudioSource>();
		swimming.Stop();
		//Start ���� �� ���ӸŴ����� ��ϵ� �÷��̾��� ������ٵ�� Frog_Move ��ũ��Ʈ ������

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			AudioManager.Instance.PlaySFX(contactWaterClip);
			swimming.Play();
			Debug.Log("���Ҹ�");
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
			if (swimming.isPlaying == false)
			{
				swimming.Play();
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
			swimming.Stop();
		}
	}
}

