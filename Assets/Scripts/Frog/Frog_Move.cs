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
	private InputAction moveAction;

	public float moveSpeed;
	public float onAirSpeed;
	public float inWaterSpeed;

	public Vector2 moveInput;
	private Vector3 actualMoveDir;
	private Vector3 inputMoveDir;
	private float tempTime;
	public float force;
	public float lillyForce;

	private bool isMove;
	public bool isGround;
	public bool isWater; //Water���� ����ϱ� ���� public

	public bool isPressed;

	public LayerMask groundLayer;

	public AudioClip jumpClip;
	public AudioClip lillypadClip;
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		moveAction = GetComponent<PlayerInput>().actions.FindAction("Move");
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
		tempTime = 0;
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
		if (isMove)
		{
			Move();
		}
		//Debug.Log($"���� �ӷ� : {rb.velocity.magnitude}");

		//childPos.position = transform.position;
		if (!isGround & !isWater)
		{
			//Debug.LogWarning("�߷°�ȭ��");
			//TODO : ��ü �߷��� �ƴ�, ������Ʈ�� �߷��� �ٲ�� �����ؾ���.
			rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);
		}
	}

	private void OnMoveEvent(Context value)
	{
		moveInput = value.ReadValue<Vector2>();
		isPressed = moveInput != Vector2.zero;
	}
	private void Move()
	{
		if (isPressed)
		{
			inputMoveDir = new Vector3(-moveInput.y, 0, moveInput.x) * moveSpeed;
			actualMoveDir = transform.TransformDirection(inputMoveDir);


			if (isWater == false && isGround && tempTime >= 0.5f && isMove)
			{
				Jump();
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
		UpdateTempTime();
	}

	private void UpdateTempTime()
	{
		if (isPressed == false)
		{
			tempTime = 0;
		}
		else if (isPressed)
		{
			tempTime += Time.deltaTime;
		}
	}

	public void Jump()
	{
		rb.AddForce(actualMoveDir * force, ForceMode.Impulse);
		rb.AddForce(Vector3.up * 200f, ForceMode.Impulse);
		GameManager.Instance.player.frogAction.jumpCount--;
		isMove = false;
		AudioManager.Instance.PlaySFX(jumpClip);
	}

	private void ResetJumpCount()
	{
		GameManager.Instance.player.frogAction.jumpCount = 0;
		if (DataManager.Instance.jumpCount == 2)
		{
			GameManager.Instance.player.frogAction.jumpCount = 1;
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
			rb.AddForce((transform.up * lillyForce) + ((-transform.right) * (lillyForce / 2)), ForceMode.Impulse);
			//NOTE : ���� �߰�
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
			ResetJumpCount();
		}
		if (collision.gameObject.layer == LayerMask.NameToLayer("LillyPad"))
		{
			lillypadContacted = false;
			ResetJumpCount();
		}
	}
}

