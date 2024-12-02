using UnityEngine;

public class Water : MonoBehaviour
{
	[Header("부력, 저항")]
	public float buoyancyForce = 30f;
	public float waterDrag = 5f;
	//private Rigidbody rb;
	//private Frog_Move frog_Move;

	private void Start()
	{
		//Start 됐을 때 게임매니저에 등록된 플레이어의 리지드바디와 Frog_Move 스크립트 가져옴
		//rb = GameManager.Instance.player.GetComponent<Rigidbody>();
		//frog_Move = GameManager.Instance.player.GetComponent<Frog_Move>();
	}

	private void OnTriggerStay(Collider other)
	{
		////레이어로 비교하는 걸로 변경해봤습니다.
		//if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		//{
		//	frog_Move.isWater = true;
		//	Vector3 force = Vector3.up * buoyancyForce;
		//	rb.AddForce(force, ForceMode.Acceleration);

		//	//물의 저항 효과
		//	rb.velocity *= 1f - (waterDrag * Time.deltaTime);
		//}


		//Debug.Log("OnTriggerStay진입");
		if (other.CompareTag("Player"))
		{
			//Debug.Log("OnTriggerStay 외부 if문 진입");
			if (other.TryGetComponent(out Rigidbody rb) && other.TryGetComponent(out Frog_Move move))
			{
				//Debug.Log("OnTriggerStay 내부 if문 진입");

				move.isWater = true;
				Vector3 force = Vector3.up * buoyancyForce;
				rb.AddForce(force, ForceMode.Acceleration);

				//물의 저항 효과
				rb.velocity *= 1f - (waterDrag * Time.deltaTime);
			}
		}
	}


	//private void OnTriggerExit(Collider other)
	//{
	//	if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
	//	{
	//		frog_Move.isWater = false;
	//	}
			
	//}
}

