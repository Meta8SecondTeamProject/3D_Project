using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : MonoBehaviour
{
	[SerializeField][Header("���� �������� �� ��")] private float forwardForce = 10f;
	[SerializeField][Header("���� �������� �� ��")] private float upForce = 40f;

	private void OnCollisionEnter(Collision other)
	{
		//Debug.Log($"Collision ������ : {other.gameObject.name}");

		//if (other.gameObject.CompareTag("Player"))
		//{
		//	//Debug.Log($"Player ������ : {other.gameObject.name}");

		//	if (other.gameObject.TryGetComponent(out Rigidbody rb))
		//	{
		//		//���� ���� ���ʹ� transform.forward * vertical + transform.right * horizontal������, 
		//		//�츮 ���� ���ʹ� �������� Rotation�� ���������� �����Ƿ� transform.right * -vertical�� �����
		//		//rb.velocity=new Vector3(rb.velocity.x,0,rb.velocity.z);
		//		//rb.velocity += new Vector3(rb.velocity.x, upForce, rb.velocity.z) + (rb.transform.right * -forwardForce);
		//		rb.AddForce(Vector3.right * upForce, ForceMode.Impulse);
		//		//rb.AddForce(rb.transform.up * upForce, ForceMode.VelocityChange);
		//	}
		//}
	}
}
