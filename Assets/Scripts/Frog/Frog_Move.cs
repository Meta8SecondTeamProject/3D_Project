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

	//[HideInInspector] public Vector2 inputValue; // �Է� ��
	//private Vector3 moveDir;
	//private Vector3 inputDir;

	//[Header("���� üũ��")]
	//public Trans	form groundCheck;
	//public LayerMask groundMask;
	//public LayerMask waterMask;

	//[Header("�̵� �� ���� ����(3, 3)")]
	public float moveSpeed;
	public float onAirSpeed;
	//public float jumpForce;
	//public float maxVelocity;
	//private float jumpCharge;

	//[Header("����׿� �ӵ� ǥ�ð�")]
	//public Text text;

	//MaxVelocity ���ѿ� bool ����

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
		//������ �ʹ� �ö󰡴ϱ� ��Ʈ�� �߿��� ¿� �ӽ÷� 60���� ����
		//Application.targetFrameRate = 60;
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

	private void Update()
	{
		//FrogState();
		//SpeedLimiter();
	}

	private void FixedUpdate()
	{
		Move();
		childPos.position = transform.position;
		////maxVelocity = isJumpByShot ? 30 : 15;
		////moveSpeed = frogAction.isJumping ? jumpSpeed : moveSpeed;

		//if (rb.velocity.magnitude > maxVelocity)
		//{
		//	rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
		//}

		////Event���� ȣ���ϴ� WASD�������� ���콺 ���⿡ �°� �ٲ�� ������ �־ Update�� �̵�
		//inputDir = new Vector3(inputValue.x, 0, inputValue.y).normalized;
		//moveDir = transform.TransformDirection(inputDir) * moveSpeed;

		//if (readyToJump == false && inputValue.magnitude > 0)
		//{
		//	Move();
		//}
		//if (grounded && inputValue.magnitude > 0)
		//{
		//	//TODO : (�Ϸ��)Invoke�� �ϸ� �ǵ��ѰŶ� �ٸ� �������� �߻���, ���� dletaTime�� �������Ѽ� 0.5�ʸ� �����ϴ�, InputSystem�� �ǵ帮�� ���� �ٲٱ�
		//	//Invoke("Jump", 0.5f);
		//	Jump();
		//}
	}



	#region �������۾�
	private void OnMoveEvent(Context value)
	{
		//�Է¹��� Ű WASD�� �������� ���� �ʱ�ȭ
		input = value.ReadValue<Vector2>();
		isPressed = input != Vector2.zero;

	}


	#endregion
	private void Jump()
	{
		//if (readyToJump == false)
		//{
		//	jumpCharge = 0;
		//	return;
		//}

		//jumpCharge += Time.deltaTime;

		//if (jumpCharge > 0.6f)
		//{
		//	//���� Rigidbody�� y�� �ӵ��� 0���� �ʱ�ȭ�ؼ� ���� ���̰� �̻������� ���� ����
		//	rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
		//	//rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

		//	Vector3 jumpDir = (moveDir * moveSpeed) + (Vector3.up * jumpForce);
		//	rb.AddForce(jumpDir.normalized * jumpForce, ForceMode.Impulse);

		//	jumpCharge = 0;	
		//}



	}
	private void Move()
	{

		#region �������۾�
		if (isPressed)
		{
			Vector3 inputMoveDir = new Vector3(input.y, 0, input.x) * moveSpeed;
			Vector3 actualMoveDir = transform.TransformDirection(inputMoveDir);
			if (isWater == false && isGround && tempTime >= 0.5f && isMove && frogAction.isJumping != true)
			{
				Vector3 moveDir = frogAction.jumpDir.position - transform.position;
				Debug.Log("�� �����ΰ�");
				rb.AddForce(moveDir * force, ForceMode.Impulse);
				//rb.AddForce(actualMoveDir * 100, ForceMode.Force);
				isMove = false;
			}
			else if (isWater)
			{
				rb.AddForce(actualMoveDir, ForceMode.Acceleration);
			}
			rb.AddForce(actualMoveDir * onAirSpeed);

		}
		if (isPressed == false)
		{
			tempTime = 0;
		}
		else if (isPressed)
		{
			tempTime += Time.deltaTime;
		}
		#endregion
		////���� ��ǥ �������� �̵� ���� ���� ���
		//Vector3 moveDirInMove = (transform.forward * input.y) + (transform.right * input.x);

		////���� �̵� ������ ���� �������� üũ
		//float dotProduct = Vector3.Dot(transform.forward, moveDirInMove.normalized);

		//if (dotProduct < 0) //���� ������ �ƴ϶��
		//{
		//	//���� �Է��� ��ȿȭ
		//	moveDirInMove = Vector3.zero;
		//}

		////��ǥ�ӵ� ��� (x, z�ุ)
		//Vector3 targetVelocity = moveDirInMove.normalized * maxVelocity;

		////���� y�� �ӵ� ����
		//float currentYVelocity = rb.velocity.y;

		////x, z�� �ӵ��� ����
		//Vector3 smoothedVelocity = Vector3.Lerp(new Vector3(rb.velocity.x, 0, rb.velocity.z), targetVelocity, Time.deltaTime * 2f);

		//rb.velocity = new Vector3(smoothedVelocity.x, currentYVelocity, smoothedVelocity.z);

		////Debug.Log($"MoveDirInMove: {moveDirInMove}");
		////Debug.Log($"DotProduct: {dotProduct}");
		////Debug.Log($"Velocity: {rb.velocity}");
		///






	}

	private void FrogState()
	{
		//grounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);

		//readyToJump = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);

		//if (grounded == false)
		//{
		//	isWater = Physics.CheckSphere(groundCheck.position, 0.2f, waterMask);
		//}
	}

	private void SpeedLimiter()
	{
		//if (grounded)
		//{
		//	maxVelocity = 15f;
		//}
		//else if (isWater)
		//{
		//	maxVelocity = 10f;
		//}
		//else
		//{
		//	maxVelocity = 30f;
		//}
	}

	private void OnDrawGizmos()
	{
		//Gizmos.color = Color.red;
		//Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
	}


	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
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

//1. (�Ϸ��) WASD�� ������ �� ������ ���� �ʰ� ���� �������� �����̽��ٸ� ������ �� ó�� AddFroce�޼��尡 ����ǵ��� ���� 
//2. (�Ϸ��) velocity�� �ִ� velocity���� �����ϱ������ �ð� �����ϱ�, ���� �ʹ� ������ Lerf �޼��� Ȱ��


