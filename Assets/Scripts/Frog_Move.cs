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

	//[HideInInspector] public Vector2 inputValue; // 입력 값
	//private Vector3 moveDir;
	//private Vector3 inputDir;

	//[Header("지면 체크용")]
	//public Transform groundCheck;
	//public LayerMask groundMask;
	//public LayerMask waterMask;

	//[Header("이동 및 점프 관련(3, 3)")]
	public float moveSpeed;
	//public float jumpForce;
	//public float maxVelocity;
	//private float jumpCharge;

	//[Header("디버그용 속도 표시계")]
	//public Text text;

	//MaxVelocity 제한용 bool 변수

	private Vector2 input;
	private float tempTime;
	public float force;

	private bool isGround;
	[HideInInspector] public bool isWater; //Water에서 사용하기 위해 public
	[HideInInspector] public bool readyToJump; //FrogAction에서 사용하기 위해 public, State랑 관계없음


	public bool isPressed;

	private KeyCode key;
	private Vector3 idleDir;

	private void Awake()
	{
		//프레임 너무 올라가니까 노트북 발열이 쩔어서 임시로 60으로 제한
		//Application.targetFrameRate = 60;
		rb = GetComponent<Rigidbody>();
		controlDefine = GetComponent<PlayerInput>().actions;
		moveAction = controlDefine.FindAction("Move");
		//frogAction = GetComponent<Frog_Action>();
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
	}

	private void Update()
	{
		//FrogState();
		//SpeedLimiter();
	}

	private void FixedUpdate()
	{
		Move();

		////maxVelocity = isJumpByShot ? 30 : 15;
		////moveSpeed = frogAction.isJumping ? jumpSpeed : moveSpeed;

		//if (rb.velocity.magnitude > maxVelocity)
		//{
		//	rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
		//}

		////Event에서 호출하니 WASD누를때만 마우스 방향에 맞게 바뀌는 문제가 있어서 Update로 이동
		//inputDir = new Vector3(inputValue.x, 0, inputValue.y).normalized;
		//moveDir = transform.TransformDirection(inputDir) * moveSpeed;

		//if (readyToJump == false && inputValue.magnitude > 0)
		//{
		//	Move();
		//}
		//if (grounded && inputValue.magnitude > 0)
		//{
		//	//TODO : (완료됨)Invoke로 하면 의도한거랑 다른 움직임이 발생함, 추후 dletaTime을 누적시켜서 0.5초를 구현하던, InputSystem을 건드리던 로직 바꾸기
		//	//Invoke("Jump", 0.5f);
		//	Jump();
		//}
	}



	#region 김찬영작업
	private void OnMoveEvent(Context value)
	{
		//입력받은 키 WASD를 바탕으로 벡터 초기화
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
		//	//현재 Rigidbody의 y축 속도를 0으로 초기화해서 점프 높이가 이상해지는 문제 방지
		//	rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
		//	//rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

		//	Vector3 jumpDir = (moveDir * moveSpeed) + (Vector3.up * jumpForce);
		//	rb.AddForce(jumpDir.normalized * jumpForce, ForceMode.Impulse);

		//	jumpCharge = 0;	
		//}



	}
	private void Move()
	{

		#region 김찬영작업
		if (isPressed)
		{
			Vector3 inputMoveDir = new Vector3(input.y, 0, input.x) * moveSpeed;
			Vector3 actualMoveDir = transform.TransformDirection(inputMoveDir);
			if (isWater == false && isGround && tempTime >= 0.5f)
			{
				rb.AddForce(Vector3.up * force, ForceMode.VelocityChange);
				rb.AddForce(actualMoveDir, ForceMode.Acceleration);
			}
			else if(isWater)
			{
				rb.AddForce(actualMoveDir, ForceMode.Acceleration);
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
		#endregion
		////로컬 좌표 기준으로 이동 방향 벡터 계산
		//Vector3 moveDirInMove = (transform.forward * input.y) + (transform.right * input.x);

		////현재 이동 방향이 전진 방향인지 체크
		//float dotProduct = Vector3.Dot(transform.forward, moveDirInMove.normalized);

		//if (dotProduct < 0) //전진 방향이 아니라면
		//{
		//	//후진 입력을 무효화
		//	moveDirInMove = Vector3.zero;
		//}

		////목표속도 계산 (x, z축만)
		//Vector3 targetVelocity = moveDirInMove.normalized * maxVelocity;

		////기존 y축 속도 유지
		//float currentYVelocity = rb.velocity.y;

		////x, z축 속도를 보간
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

		if (collision.collider.CompareTag("Ground"))
		{
			isGround = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.collider.CompareTag("Ground"))
		{
			isGround = false;
		}
	}
}

//1. (완료됨) WASD를 눌렀을 때 점프만 하지 않고 누른 방향으로 스페이스바를 눌렀을 때 처럼 AddFroce메서드가 실행되도록 수정 
//2. (완료됨) velocity가 최대 velocity까지 도달하기까지의 시간 수정하기, 지금 너무 빠르니 Lerf 메서드 활용


