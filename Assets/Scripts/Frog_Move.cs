using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;


public class Frog_Move : MonoBehaviour
{
	private Rigidbody rb;
	private InputActionAsset controlDefine;
	private InputAction moveAction;

	[HideInInspector] public Vector2 inputValue; // 입력 값
	private Vector3 moveDir;
	private Vector3 inputDir;

	[Header("지면 체크용")]
	public Transform groundCheck;
	public LayerMask groundMask;
	private bool grounded;
	[HideInInspector] public bool readyToJump;

	[Header("이동 및 점프 관련(3, 3)")]
	public float moveSpeed;
	public float jumpForce;
	public float maxVelocity;
	private float jumpCharge;

	[Header("디버그용 속도 표시계")]
	public Text text;

	private void Awake()
	{
		//프레임 너무 올라가니까 노트북 발열이 개쩔어서 임시로 60으로 제한
		Application.targetFrameRate = 60;

		rb = GetComponent<Rigidbody>();
		controlDefine = GetComponent<PlayerInput>().actions;
		moveAction = controlDefine.FindAction("Move");
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
		StateHandler();

		//Event에서 호출하니 WASD누를때만 마우스 방향에 맞게 바뀌는 문제가 있어서 Update로 이동
		inputDir = new Vector3(inputValue.x, 0, inputValue.y).normalized;
		moveDir = transform.TransformDirection(inputDir) * moveSpeed;

		if (readyToJump == false && inputValue.magnitude > 0)
		{
			Move();
		}
		if (grounded && inputValue.magnitude > 0)
		{
			//TODO : (완료됨)Invoke로 하면 의도한거랑 다른 움직임이 발생함, 추후 dletaTime을 누적시켜서 0.5초를 구현하던, InputSystem을 건드리던 로직 바꾸기
			//Invoke("Jump", 0.5f);
			Jump();
		}


		//Debug.Log(grounded);
		//Debug.Log(inputValue.x); //A : -1 / D : 1
		//Debug.Log(inputValue.y); //W : 1 / S : 1 
		//Debug.Log(inputValue);
		//Debug.Log(jumpCharge);
		text.text = $"current Speed : {(rb.velocity.magnitude).ToString("F2")}km/h";
	}

	private void OnMoveEvent(InputAction.CallbackContext context)
	{
		//입력받은 키 WASD를 바탕으로 벡터 초기화
		inputValue = context.ReadValue<Vector2>();
	}

	private void Jump()
	{
		if (readyToJump == false)
		{
			jumpCharge = 0;
			return;
		}

		jumpCharge += Time.deltaTime;

		if (jumpCharge > 0.4f)
		{
			//현재 Rigidbody의 y축 속도를 0으로 초기화해서 점프 높이가 이상해지는 문제 방지
			rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
			rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
			jumpCharge = 0;
		}
	}

	private void Move()
	{
		rb.velocity += new Vector3(moveDir.x, 0, moveDir.z) * Time.deltaTime * moveSpeed;
	}

	private void StateHandler()
	{
		//점프와 그라운드의 기준을 다르게함
		//0.4f짜리 readyToJump 그대로 쓰자니 바닥이랑 가까이 있을때 움직이지 못하는 현상 방지용
		//grounded를 0.05로 하면 안됐나요?
		//그럼 원본게임이랑 조금 다르게 움직임
		grounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);
		readyToJump = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);
	}

	private void OnDrawGizmos()
	{
		//Gizmos.color = Color.green;
		//Gizmos.DrawWireSphere(groundCheck.position, 0.4f);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
	}
}