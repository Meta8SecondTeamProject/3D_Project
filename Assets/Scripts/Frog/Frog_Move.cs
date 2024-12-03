using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;
using System.Collections;


[RequireComponent(typeof(PlayerInput))]
public class Frog_Move : MonoBehaviour
{
	private Rigidbody rb;
	private InputActionAsset controlDefine;
	private InputAction moveAction;
	private Frog_Action frogAction;
	public Transform childPos;

	public float moveSpeed;
	public float onAirSpeed;

	private Vector2 input;
	private float tempTime;
	public float force;

	public bool isMove;
	public bool isGround;
	public bool isWater; //Water���� ����ϱ� ���� public
	[HideInInspector] public bool readyToJump; //FrogAction���� ����ϱ� ���� public, State�� �������


	public bool isPressed;

	public LayerMask groundLayer;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		controlDefine = GetComponent<PlayerInput>().actions;
		moveAction = controlDefine.FindAction("Move");
		frogAction = GetComponent<Frog_Action>();
	}

	private void OnEnable()
	{
		moveAction.performed += OnMoveEvent;
		moveAction.canceled += OnMoveEvent;

	}

	private void OnDisable()
	{
		moveAction.performed -= OnMoveEvent;
		moveAction.canceled -= OnMoveEvent;
	}

	private void Start()
	{
		isMove = true;
		StartCoroutine(MoveCoroutine());
	}

	private IEnumerator MoveCoroutine()
	{
		while (true)
		{
			yield return new WaitWhile(() => isMove);
			yield return new WaitForSeconds(0.2f);
			isMove = true;
		}
	}
	private void FixedUpdate()
	{
		Move();
		//childPos.position = transform.position;
		if (!isGround & !isWater)
		{
			Debug.LogWarning("�߷°�ȭ��");
			Physics.gravity -= new Vector3(0, 0.2f, 0);
		}
	}
	private void OnMoveEvent(Context value)
	{
		input = value.ReadValue<Vector2>();
		isPressed = input != Vector2.zero;
	}
	private void Move()
	{

		if (isPressed)
		{
			Vector3 inputMoveDir = new Vector3(-input.y, 0, input.x) * moveSpeed;
			Vector3 actualMoveDir = transform.TransformDirection(inputMoveDir);


			if (isWater == false && isGround && tempTime >= 0.5f && isMove && frogAction.isJumping != true)
			{
				Debug.Log("�� �����ΰ�");
				rb.AddForce(Vector3.up * force, ForceMode.VelocityChange);
				//rb.AddForce(actualMoveDir * 100, ForceMode.Force);
				isMove = false;
			}
			else if (isWater)
			{
				rb.AddForce(actualMoveDir, ForceMode.Force);
			}
			else if (!isGround)
			{
				rb.AddForce(actualMoveDir * onAirSpeed, ForceMode.Force);
			}
		}
		if (isPressed == false)
		{
			tempTime = 0;
		}
		else if (isPressed)
		{
			tempTime += Time.deltaTime;
		}
	}


	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			Physics.gravity = new Vector3(0, -20, 0);
			Debug.Log("hi");
			isGround = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			Debug.Log("hi");
			isGround = false;
		}
	}

}

