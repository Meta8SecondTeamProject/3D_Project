using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class Frog_Action : MonoBehaviour
{
	[Header("����(�ѱ�)�� �ӽ÷� ������ ������ �־��ּ���")]
	public Transform muzzlePos;
	public Camera shotPoint;
	public Transform knockbackPos;
	public Transform jumpDir;
	public ParticleSystem particle;
	private Rigidbody rb;
	private CinemachineVirtualCamera virtualCamera;
	private CinemachineBasicMultiChannelPerlin noise;
	private InputActionAsset controlDefine;
	private InputAction jumpAction;
	private InputAction fireAction;
	private InputAction moveAction;

	[Header("�ݵ����� ���� �˹�, ����, ��鸲")]
	[Range(0, 100)]
	public float knockbackForce;
	public float jumpForce;
	public float shakeDuration;
	public float shakePower;
	//public float shakeOffset;
	private float shakeTimer = 0.5f;

	private float jumpInput;
	public int jumpCount;
	public bool isJumping;
	private bool fireCooldown;
	private Vector2 input;

	private Frog_Move frogMove;
	public LayerMask groundMask;

	public RawImage crossHair;
	public GameObject projectile;

	private void Awake()
	{
		rb = GetComponentInParent<Rigidbody>();
		virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
		noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		controlDefine = GetComponent<PlayerInput>().actions;
		jumpAction = controlDefine.FindAction("Jump");
		fireAction = controlDefine.FindAction("Fire");
		moveAction = controlDefine.FindAction("Move");
		frogMove = GetComponent<Frog_Move>();
	}

	private void OnEnable()
	{
		fireAction.performed += OnClickEvent;
		fireAction.canceled += OnClickEvent;
		jumpAction.performed += OnJumpEvent;
		jumpAction.canceled += OnJumpEvent;
	}
	private void OnDisable()
	{
		fireAction.performed -= OnClickEvent;
		fireAction.canceled -= OnClickEvent;
		jumpAction.performed -= OnJumpEvent;
		jumpAction.canceled -= OnJumpEvent;
	}

	private void Start()
	{
		shakeTimer = shakeDuration;
		fireCooldown = true;
		StartCoroutine(FireCooldown());
	}

	private IEnumerator FireCooldown()
	{
		while (true)
		{
			Debug.Log($"coroutine : {fireCooldown}");
			yield return new WaitWhile(() => fireCooldown);
			yield return new WaitForSeconds(0.3f);
			fireCooldown = true;
		}

	}

	private void Update()
	{
		if (shakeDuration > 0)
		{
			//��鸲 �������� �� ������ ����
			shakeDuration -= Time.deltaTime;

			//���ӽð��� ������ ��鸲 ������ 0���� �ʱ�ȭ
			if (shakeDuration <= 0f && noise != null)
			{
				noise.m_AmplitudeGain = 0f;
				noise.m_FrequencyGain = 0f;
			}
		}

	}

	private bool canJump = true;
	private void FixedUpdate()
	{
		if (frogMove.isGround)
		{
			jumpCount = DataManager.Instance.jumpCount;
			Debug.Log("����ī��Ʈ 2");
		}
	}

	private void OnClickEvent(Context context)
	{
		Debug.Log("Ŭ�� ����");
		//���콺 ��Ŭ�� �Է��� �����Ǹ�
		Debug.Log($"Fireinput : {fireCooldown}");
		if (context.ReadValue<float>() > 0 && fireCooldown)
		{
			Physics.gravity = new Vector3(0, -20, 0);
			particle.Play(true);
			Vector3 knockbackdir = knockbackPos.position - muzzlePos.position;

			rb.AddForce(knockbackdir * knockbackForce, ForceMode.Impulse);

			//ī�޶� ��鸲 ���� �ʱ�ȭ
			shakeDuration = shakeTimer;

			//ī�޶� ��鸲 �޼��� ����
			ShakeCamera(shakePower, shakeDuration);
			fireCooldown = false;
		}
	}

	private void OnJumpEvent(Context context)
	{
		//�� �̷� ������ ��������~~~
		//2�� ���� �ȵǴ°� �����ؾ���
		//WASD������ �� �ش��ϴ� �������� �����ϴµ�,
		//�������� �ʹ� ���ϴϱ� ������ force�� ã�ƾ���.
		jumpInput = context.ReadValue<float>();
		//if (frogMove.isGround) jumpCount = 2;
		isJumping = jumpInput != 0;
		if (isJumping && jumpCount >= 1)
		{
			Vector3 inputMoveDir = new Vector3(-frogMove.input.y, 0, frogMove.input.x);
			Vector3 actualMoveDir = transform.TransformDirection(inputMoveDir);
			if (!frogMove.isGround)
			{

				Debug.Log("����ī��Ʈ 1");
				jumpCount = 1;
				rb.AddForce(actualMoveDir * jumpForce, ForceMode.Impulse);
				jumpCount--;
			}
			else
			{
				Debug.Log("��������");
				Vector3 forwardDir = jumpDir.position - transform.position;
				rb.AddForce((actualMoveDir + Vector3.up) * jumpForce, ForceMode.Impulse);
				jumpCount--;
				frogMove.isGround = false;
			}
		}
	}
	private void ShakeCamera(float shakePower, float shakeDuration)
	{
		noise.m_PivotOffset = Vector3.one;// * shakeOffset;
		noise.m_AmplitudeGain = shakePower;
		noise.m_FrequencyGain = shakeDuration;
	}

}
