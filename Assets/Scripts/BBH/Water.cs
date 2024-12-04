using UnityEngine;

public class Water : MonoBehaviour
{
	[Header("�η�, ����")]
	public float buoyancyForce = 30f;
	public float waterDrag = 5f;


	private void Start()
	{
		//Start ���� �� ���ӸŴ����� ��ϵ� �÷��̾��� ������ٵ�� Frog_Move ��ũ��Ʈ ������

	}

	private void OnTriggerStay(Collider other)
	{
		//���̾�� ���ϴ� �ɷ� �����غý��ϴ�.
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			Physics.gravity = new Vector3(0, -20, 0);
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

	}
}

