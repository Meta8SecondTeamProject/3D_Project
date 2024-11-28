using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;


public class Frog_Move : MonoBehaviour
{
	private Rigidbody rb;
	private InputActionAsset controlDefine;
	private InputAction moveAction;

	[HideInInspector] public Vector2 inputValue; // �Է� ��
	private Vector3 moveDir;
	private Vector3 inputDir;

	[Header("���� üũ��")]
	public Transform groundCheck;
	public LayerMask groundMask;
	private bool grounded;
	[HideInInspector] public bool readyToJump;

	[Header("�̵� �� ���� ����(3, 3)")]
	public float moveSpeed;
	public float jumpForce;
	public float maxVelocity;
	private float jumpCharge;
	private float jumpSpeed;


	[Header("����׿� �ӵ� ǥ�ð�")]
	public Text text;

	private Frog_Action frogAction;
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
		//jumpSpeed = moveSpeed;// * 2f;
	}

    private void Update()
    {
        StateHandler();
        text.text = $"current Speed : {(rb.velocity.magnitude).ToString("F2")}";
    }

    private void FixedUpdate()
	{
		
		maxVelocity = frogAction.isJumping ? 30 : 15;
		//moveSpeed = frogAction.isJumping ? jumpSpeed : moveSpeed;

		if (rb.velocity.magnitude > maxVelocity)
		{
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
		}

		//Event���� ȣ���ϴ� WASD�������� ���콺 ���⿡ �°� �ٲ�� ������ �־ Update�� �̵�
		inputDir = new Vector3(inputValue.x, 0, inputValue.y).normalized;
		moveDir = transform.TransformDirection(inputDir) * moveSpeed;

		if (readyToJump == false && inputValue.magnitude > 0)
		{
			Move();
		}
		if (grounded && inputValue.magnitude > 0)
		{
			//TODO : (�Ϸ��)Invoke�� �ϸ� �ǵ��ѰŶ� �ٸ� �������� �߻���, ���� dletaTime�� �������Ѽ� 0.5�ʸ� �����ϴ�, InputSystem�� �ǵ帮�� ���� �ٲٱ�
			//Invoke("Jump", 0.5f);
			Jump();
		}


		//Debug.Log(grounded);
		//Debug.Log(inputValue.x); //A : -1 / D : 1
		//Debug.Log(inputValue.y); //W : 1 / S : -1 
		//Debug.Log(inputValue);
		//Debug.Log(jumpCharge);
		//Debug.Log(rb.velocity.z);

		//Debug.Log($"moveDir.x : {moveDir.x}");
		//Debug.Log($"moveDir.y : {moveDir.y}");
		//Debug.Log($"moveDir.z : {moveDir.z}");

		//.Log($"velocity.x : {rb.velocity.x}");
		//Debug.Log($"velocity.y : {rb.velocity.y}");
		//Debug.Log($"velocity.z : {rb.velocity.z}");

        //Debug.Log($"moveDir.z �� 10% : {Mathf.Abs(moveDir.z * 0.9f)}");

        
	}

	private void OnMoveEvent(InputAction.CallbackContext context)
	{
		//�Է¹��� Ű WASD�� �������� ���� �ʱ�ȭ
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

		if (jumpCharge > 0.6f)
		{
			//���� Rigidbody�� y�� �ӵ��� 0���� �ʱ�ȭ�ؼ� ���� ���̰� �̻������� ���� ����
			rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
			//rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

            Vector3 jumpDir = (moveDir * moveSpeed) + (Vector3.up * jumpForce);
            rb.AddForce(jumpDir.normalized * jumpForce, ForceMode.Impulse);

            jumpCharge = 0;
		}
    }

    private void Move()
    {
        // ���� ��ǥ �������� �̵� ���� ���� ���
        Vector3 moveDirInMove = (transform.forward * inputValue.y) + (transform.right * inputValue.x);

        // ���� �̵� ������ ���� �������� üũ (���� �� ����)
        float dotProduct = Vector3.Dot(transform.forward, moveDirInMove.normalized);

        if (dotProduct < 0) // ���� ������ �ƴ϶�� (���� ����)
        {
            // ���� ���� �Է��� ��ȿȭ
            moveDirInMove = Vector3.zero;
        }

        // ��ǥ �ӵ� ��� (x, z�ุ)
        Vector3 targetVelocity = moveDirInMove.normalized * maxVelocity;

        // ���� y�� �ӵ� ����
        float currentYVelocity = rb.velocity.y;

        // x, z�� �ӵ��� Lerp�� �ε巴�� ����
        Vector3 smoothedVelocity = Vector3.Lerp(new Vector3(rb.velocity.x, 0, rb.velocity.z), targetVelocity, Time.deltaTime * 2f);

        // ���� �ӵ� ���� (y�� �ӵ��� �߷¿� ���� ��� ����)
        rb.velocity = new Vector3(smoothedVelocity.x, currentYVelocity, smoothedVelocity.z);

        // ����׿� �α� (�ʿ��)
        // Debug.Log($"MoveDirInMove: {moveDirInMove}");
        // Debug.Log($"DotProduct: {dotProduct}");
        // Debug.Log($"Velocity: {rb.velocity}");
    }

    private void StateHandler()
	{
		//������ �׶����� ������ �ٸ�����
		//0.4f¥�� readyToJump �״�� ���ڴ� �ٴ��̶� ������ ������ �������� ���ϴ� ���� ������
		//grounded�� 0.05�� �ϸ� �ȵ� ����?
		//�׷� ���������̶� ���� �ٸ��� ������
		grounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);
		readyToJump = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);

		if (!frogAction.isJumping)
			return;
		frogAction.isJumping = !Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
	}
}

//1. (�Ϸ��) WASD�� ������ �� ������ ���� �ʰ� ���� �������� �����̽��ٸ� ������ �� ó�� AddFroce�޼��尡 ����ǵ��� ���� 
//2. velocity�� �ִ� velocity���� �����ϱ������ �ð� �����ϱ�, ���� �ʹ� ������ Lerf �޼��� Ȱ��
 


