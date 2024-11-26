using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FliesMovement : MonoBehaviour
{
	public float upSpeed;
	public float YAmplitude;
	public Transform rotateTarget;
	public float rotateSpeed;
	private void Start()
	{

	}
	private void Update()
	{
		float dir = Mathf.Sin(Time.time * upSpeed) * YAmplitude * 0.01f;
		transform.position = new Vector3(transform.position.x, transform.position.y + dir, transform.position.z);
		transform.RotateAround(rotateTarget.position, Vector3.up, rotateSpeed);
	}
}
