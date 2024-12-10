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

	public AudioClip jumpClip;
	public AudioClip lillypadClip;
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
			rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);
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
			inputMoveDir = new Vector3(-input.y, 0, input.x) * moveSpeed;
			actualMoveDir = transform.TransformDirection(inputMoveDir);


			if (isWater == false && isGround && tempTime >= 0.5f && isMove)
			{
				rb.AddForce(actualMoveDir * force, ForceMode.Impulse);
				rb.AddForce(Vector3.up * 200f, ForceMode.Impulse);
				frogAction.jumpCount--;
				isMove = false;
				AudioManager.Instance.PlaySFX(jumpClip,0.75f);
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

	private bool lillypadContacted;

	private void OnCollisionEnter(Collision collision)
	{
		if (lillypadContacted == true)
			return;

		if (collision.gameObject.layer == LayerMask.NameToLayer("LillyPad"))
		{
			lillypadContacted = true;
			rb.AddForce(Vector3.up * lillyForce, ForceMode.Impulse);
			//NOTE : 사운드 추가
			AudioManager.Instance.PlaySFX(lillypadClip);
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			isGround = true;
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
				frogAction.jumpCount = 1;
			}
		}
		if (collision.gameObject.layer == LayerMask.NameToLayer("LillyPad"))
		{
            lillypadContacted = false;

            frogAction.jumpCount = 0;
			if (DataManager.Instance.jumpCount == 2)
			{

                frogAction.jumpCount = 1;
			}
		}
	}

}

