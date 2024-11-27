using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UniRan = UnityEngine.Random;

public class FliesMovement : MonoBehaviour
{
	[Range(0, 1)]
	public float upSpeed;
	[Range(0, 1)]
	public float YAmplitude;
	[Range(0, 1)]
	public float rotateSpeed;

	public Transform rotateTarget;

	public int rand;
	private float dir;

	public Mesh mesh;
	public Vector3 gizmoPoint;

	private void Start()
	{
		gizmoPoint = rotateTarget.position;
		rand = UniRan.Range(0, 2);
	}
	private void Update()
	{
		Movement();
	}
	private void Movement()
	{
		if (rand == 0)
		{
			dir = Mathf.Sin(Time.time * upSpeed) * YAmplitude * 0.1f;

		}
		else
		{
			dir = Mathf.Cos(Time.time * upSpeed) * YAmplitude * 0.1f;
		}
		transform.position = new Vector3(transform.position.x, transform.position.y + dir, transform.position.z);
		transform.RotateAround(rotateTarget.position, Vector3.down, rotateSpeed);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireMesh(mesh, gizmoPoint, Quaternion.identity, new Vector3(35, 10, 35));
	}

}
