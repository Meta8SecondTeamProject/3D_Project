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
	public float inWaterSpeed;

	public Vector2 input;
	private Vector3 actualMoveDir;
	private Vector3 inputMoveDir;
	private float tempTime;
	public float force;
	public float lillyForce;

	public bool isMove;
	public bool isGround;
	public bool isWater; //Water에서 사용하기 위해 public
	[HideInInspector] public bool readyToJump; //FrogAction에서 사용하기 위해 public, State랑 관계없음


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

		//Debug.Log($"현재 속력 : {rb.velocity.magnitude}");

		//childPos.position = transform.position;
		if (!isGround & !isWater)
		{
			//Debug.LogWarning("중력강화중");
			//TODO : 전체 중력이 아닌, 오브젝트만 중력이 바뀌도록 수정해야함.
			rb.AddForce(Vector3.down * 15f, ForceMode.Acceleration);
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
			inputMoveDir = new Vector3(-input.y, 1, input.x) * moveSpeed;
			actualMoveDir = transform.TransformDirection(inputMoveDir);


			if (isWater == false && isGround && tempTime >= 0.5f && isMove)
			{
				rb.AddForce(actualMoveDir * force, ForceMode.Impulse);
				frogAction.jumpCount--;
				isMove = false;
			}
			else if (isWater)
			{
				rb.AddForce(actualMoveDir * inWaterSpeed, ForceMode.Force);
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

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("LillyPad"))
		{
			//TODO : 여기 중력값 수정되는것도 변경해야함.
			//Physics.gravity = new Vector3(0, -20, 0);
			Vector3 actualMoveDir = transform.TransformDirection(inputMoveDir);
			rb.AddForce(actualMoveDir * lillyForce, ForceMode.Impulse);
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			isGround = true;
			//Physics.gravity = new Vector3(0, -20, 0);
		}

	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			isGround = false;
			frogAction.jumpCount = 0;
			if (DataManager.Instance.jumpCount == 2)
			{
				Debug.Log("점프강화됐네?");
				frogAction.jumpCount = 1;
			}
		}
		if (collision.gameObject.layer == LayerMask.NameToLayer("LillyPad"))
		{
			frogAction.jumpCount = 0;
			if (DataManager.Instance.jumpCount == 2)
			{
				frogAction.jumpCount = 1;
			}
		}
	}

}

