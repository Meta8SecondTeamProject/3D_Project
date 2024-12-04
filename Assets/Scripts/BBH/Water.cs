using UnityEngine;

public class Water : MonoBehaviour
{
	[Header("부력, 저항")]
	public float buoyancyForce = 30f;
	public float waterDrag = 5f;


	private void Start()
	{
		//Start 됐을 때 게임매니저에 등록된 플레이어의 리지드바디와 Frog_Move 스크립트 가져옴

	}

	private void OnTriggerStay(Collider other)
	{
		//레이어로 비교하는 걸로 변경해봤습니다.
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

