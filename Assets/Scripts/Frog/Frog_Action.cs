using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class Frog_Action : MonoBehaviour
{
	[Header("����(�ѱ�)�� �ӽ÷� ������ ������ �־��ּ���")]
	public Transform muzzlePos;
	public Transform shotPoint;
	public Transform shotDir;
	public Transform knockbackPos;
	public Transform jumpDir;

	public ParticleSystem fire_Particle;
	public ParticleSystem smoke_Particle;


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
	public float fireForce;

	private float jumpInput;
	public int jumpCount;
	public bool isJumping;
	private bool fireCooldown;
	private Vector2 input;

	private Frog_Move frogMove;
	private Frog_Look frogLook;
	public LayerMask groundMask;

	public RawImage crossHair;
	public GameObject projectile;

	private ObjectPool pool;

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

		pool = FindAnyObjectByType<ObjectPool>();
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
			//Debug.Log($"coroutine : {fireCooldown}");
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
	private void FixedUpdate()
	{
		if (frogMove.isGround)
		{
			jumpCount = DataManager.Instance.jumpCount;
			//Debug.Log("����ī��Ʈ 2");
		}
	}

	private void OnClickEvent(Context context)
	{
		//Debug.Log("Ŭ�� ����");
		//���콺 ��Ŭ�� �Է��� �����Ǹ�
		//Debug.Log($"Fireinput : {fireCooldown}");
		if (context.ReadValue<float>() > 0 && fireCooldown)
		{
			Physics.gravity = new Vector3(0, -20, 0);
			fire_Particle.Play(true);
			smoke_Particle.Play(true);
			Vector3 knockbackdir = knockbackPos.position - muzzlePos.position;
			rb.AddForce(knockbackdir * knockbackForce, ForceMode.Impulse);
			//ī�޶� ��鸲 ���� �ʱ�ȭ
			shakeDuration = shakeTimer;
			//ī�޶� ��鸲 �޼��� ����
			ShakeCamera(shakePower, shakeDuration);
			fireCooldown = false;

			GameObject proj = GameManager.Instance.pool.Pop(GameManager.Instance.pool.obj[5].name);
			proj.transform.position = shotPoint.position;
			Vector3 dir = shotDir.position - shotPoint.position;
			proj.gameObject.GetComponent<Rigidbody>().AddForce(dir * fireForce, ForceMode.Impulse);
		}
	}

	private void OnJumpEvent(Context context)
	{
		jumpInput = context.ReadValue<float>();
		//if (frogMove.isGround) jumpCount = 2;
		isJumping = jumpInput != 0;
		if (frogMove.isWater) jumpCount = DataManager.Instance.jumpCount;
		if (isJumping && jumpCount >= 1)
		{
			if (!frogMove.isGround)
			{
				JumpForcing(2f);
			}
			else
			{
				JumpForcing(1f);
				frogMove.isGround = false;
			}
		}
	}
	private void JumpForcing(float y)
	{
		//Debug.Log("��������");
		Vector3 inputMoveDir = new Vector3(-frogMove.input.y, y, frogMove.input.x);
		Vector3 actualMoveDir = transform.TransformDirection(inputMoveDir);
		jumpCount--;
		rb.AddForce(actualMoveDir * jumpForce, ForceMode.Impulse);
	}

	private void ShakeCamera(float shakePower, float shakeDuration)
	{
		noise.m_PivotOffset = Vector3.one;// * shakeOffset;
		noise.m_AmplitudeGain = shakePower;
		noise.m_FrequencyGain = shakeDuration;
	}

}
