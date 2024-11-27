using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flies : MonoBehaviour
{
	public RotateTarget rotateTarget;
	private Flies flies;
	private FliesMovement fliesMovement;
	public Mesh mesh;
	private void Awake()
	{
		flies = GetComponentInChildren<Flies>();
		fliesMovement = GetComponentInChildren<FliesMovement>();
	}
	private void Start()
	{
		//if (fliesMovement.rand == 0)
		//{
		//	rotateTarget.transform.position = new Vector3(rotateTarget.transform.position.x, rotateTarget.transform.position.y + 9f, rotateTarget.transform.position.z);
		//}
		//else
		//{
		//	rotateTarget.transform.position = new Vector3(rotateTarget.transform.position.x, rotateTarget.transform.position.y - 9f, rotateTarget.transform.position.z);
		//}
	}

	private void Update()
	{

	}
	private void OnDrawGizmos()
	{
		//if (fliesMovement.rand == 0)
		//{
		//	Gizmos.color = Color.yellow;
		//}
		//else
		//{
		//	Gizmos.color = Color.blue;
		//}
		//Gizmos.DrawWireMesh(mesh, rotateTarget.transform.position, Quaternion.identity, new Vector3(35, 9, 35));

	}
}
