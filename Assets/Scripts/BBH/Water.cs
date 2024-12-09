using UnityEngine;

public class Water : MonoBehaviour
{
	[Header("�η�, ����")]
	public float buoyancyForce = 30f;
	public float waterDrag = 5f;

	public AudioSource waterAudioSource;
	public AudioClip contactWaterClip;
	public AudioClip swimmingClip;
	

	private void Start()
	{
		//Start ���� �� ���ӸŴ����� ��ϵ� �÷��̾��� ������ٵ�� Frog_Move ��ũ��Ʈ ������

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(contactWaterClip);
            waterAudioSource.clip = swimmingClip;
			waterAudioSource.loop = true;
			waterAudioSource.Play();
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
		}

		if (other.CompareTag("Player"))
		{
			waterAudioSource.Stop();
		}

	}
}

