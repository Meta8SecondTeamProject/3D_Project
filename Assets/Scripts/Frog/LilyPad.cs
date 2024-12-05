using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : MonoBehaviour
{
	[SerializeField][Header("전방 방향으로 줄 힘")] private float forwardForce = 10f;
	[SerializeField][Header("위쪽 방향으로 줄 힘")] private float upForce = 40f;

	private void OnCollisionEnter(Collision other)
	{
		//Debug.Log($"Collision 감지됨 : {other.gameObject.name}");

		//if (other.gameObject.CompareTag("Player"))
		//{
		//	//Debug.Log($"Player 감지됨 : {other.gameObject.name}");

		//	if (other.gameObject.TryGetComponent(out Rigidbody rb))
		//	{
		//		//원래 방향 벡터는 transform.forward * vertical + transform.right * horizontal이지만, 
		//		//우리 방향 벡터는 개구리의 Rotation이 정상적이이 않으므로 transform.right * -vertical로 계산함
		//		//rb.velocity=new Vector3(rb.velocity.x,0,rb.velocity.z);
		//		//rb.velocity += new Vector3(rb.velocity.x, upForce, rb.velocity.z) + (rb.transform.right * -forwardForce);
		//		rb.AddForce(Vector3.right * upForce, ForceMode.Impulse);
		//		//rb.AddForce(rb.transform.up * upForce, ForceMode.VelocityChange);
		//	}
		//}
	}
}
