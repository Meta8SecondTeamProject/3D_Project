using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class Frog_Action : MonoBehaviour
{
	[Header("머즐(총구)에 임시로 개구리 포지션 넣어주세요")]
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

	[Header("반동으로 인한 넉백, 점프, 흔들림")]
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
			//흔들림 지속지간 매 프레임 감소
			shakeDuration -= Time.deltaTime;

			//지속시간이 끝나면 흔들림 강도를 0으로 초기화
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
			Debug.Log("점프카운트 2");
		}
	}

	private void OnClickEvent(Context context)
	{
		Debug.Log("클릭 감지");
		//마우스 좌클릭 입력이 감지되면
		Debug.Log($"Fireinput : {fireCooldown}");
		if (context.ReadValue<float>() > 0 && fireCooldown)
		{
			Physics.gravity = new Vector3(0, -20, 0);
			particle.Play(true);
			Vector3 knockbackdir = knockbackPos.position - muzzlePos.position;

			rb.AddForce(knockbackdir * knockbackForce, ForceMode.Impulse);

			//카메라 흔들림 변수 초기화
			shakeDuration = shakeTimer;

			//카메라 흔들림 메서드 실행
			ShakeCamera(shakePower, shakeDuration);
			fireCooldown = false;
		}
	}

	private void OnJumpEvent(Context context)
	{
		//아 이런 ㅈ같은 점프로직~~~
		//2단 점프 안되는거 수정해야함
		//WASD눌렀을 때 해당하는 방향으로 점프하는데,
		//점프력이 너무 강하니까 적당한 force를 찾아야함.
		jumpInput = context.ReadValue<float>();
		//if (frogMove.isGround) jumpCount = 2;
		isJumping = jumpInput != 0;
		if (isJumping && jumpCount >= 1)
		{
			Vector3 inputMoveDir = new Vector3(-frogMove.input.y, 0, frogMove.input.x);
			Vector3 actualMoveDir = transform.TransformDirection(inputMoveDir);
			if (!frogMove.isGround)
			{

				Debug.Log("점프카운트 1");
				jumpCount = 1;
				rb.AddForce(actualMoveDir * jumpForce, ForceMode.Impulse);
				jumpCount--;
			}
			else
			{
				Debug.Log("점프진입");
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
