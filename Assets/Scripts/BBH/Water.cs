using UnityEngine;

public class Water : MonoBehaviour
{
	[Header("�η�, ����")]
	public float buoyancyForce = 30f;
	public float waterDrag = 5f;
	//private Rigidbody rb;
	//private Frog_Move frog_Move;

	private void Start()
	{
		//Start ���� �� ���ӸŴ����� ��ϵ� �÷��̾��� ������ٵ�� Frog_Move ��ũ��Ʈ ������
		//rb = GameManager.Instance.player.GetComponent<Rigidbody>();
		//frog_Move = GameManager.Instance.player.GetComponent<Frog_Move>();
	}

	private void OnTriggerStay(Collider other)
	{
		////���̾�� ���ϴ� �ɷ� �����غý��ϴ�.
		//if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		//{
		//	frog_Move.isWater = true;
		//	Vector3 force = Vector3.up * buoyancyForce;
		//	rb.AddForce(force, ForceMode.Acceleration);

		//	//���� ���� ȿ��
		//	rb.velocity *= 1f - (waterDrag * Time.deltaTime);
		//}


		//Debug.Log("OnTriggerStay����");
		if (other.CompareTag("Player"))
		{
			//Debug.Log("OnTriggerStay �ܺ� if�� ����");
			if (other.TryGetComponent(out Rigidbody rb) && other.TryGetComponent(out Frog_Move move))
			{
				//Debug.Log("OnTriggerStay ���� if�� ����");

				move.isWater = true;
				Vector3 force = Vector3.up * buoyancyForce;
				rb.AddForce(force, ForceMode.Acceleration);

				//���� ���� ȿ��
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

