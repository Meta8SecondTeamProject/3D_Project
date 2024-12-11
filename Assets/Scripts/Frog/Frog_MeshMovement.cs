using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_MeshMovement : MonoBehaviour
{

	public GameObject lookDir;
	Rigidbody rb;

	private void Awake()
	{
		rb = GetComponentInParent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		//transform.rotation = Quaternion.Lerp(Quaternion.Euler(),);

		//Vector3 dir = lookDir.transform.position - transform.position;
		//transform.LookAt(dir);
	}

}
