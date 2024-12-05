using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class Frog_Action : MonoBehaviour
{
	[Header("머즐(총구)에 임시로 개구리 포지션 넣어주세요")]
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

	[Header("반동으로 인한 넉백, 점프, 흔들림")]
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
	public bool canFire;
	private bool isFiring;
	private float fireInput;

	private Frog_Move frogMove;
	private Frog_Look frogLook;

	public LayerMask groundMask;

	public RawImage crossHair;
	public GameObject projectile;
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
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
		fireAction.started += OnClickEvent;
		jumpAction.performed += OnJumpEvent;
	}
	private void OnDisable()
	{
		fireAction.started -= OnClickEvent;
		jumpAction.performed -= OnJumpEvent;
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
		if (canFire)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
	private void FixedUpdate()
	{
		if (frogMove.isGround)
		{
			jumpCount = DataManager.Instance.jumpCount;
		}

	}

	private void OnClickEvent(Context context)
	{
		fireInput = context.ReadValue<float>();
		isFiring = fireInput != 0;
		//if (frogLook.escapeDown || UIManager.Instance.ga) return;
		if (UIManager.Instance.gameMenuController.pausedMenu.activeSelf == false)
		{
			canFire = true;
		}
		if (UIManager.Instance.gameMenuController.pausedMenu.activeSelf)
		{
			canFire = false;
		}

		if (canFire) Fire(isFiring);
	}

	private void Fire(bool isfiring)
	{

		if (isfiring && fireCooldown)
		{
			Physics.gravity = new Vector3(0, -20, 0);

			fire_Particle.Play(true);
			smoke_Particle.Play(true);

			Vector3 knockbackdir = knockbackPos.position - muzzlePos.position;
			rb.AddForce(knockbackdir * knockbackForce, ForceMode.Impulse);

			//카메라 흔들림 변수 초기화
			shakeDuration = shakeTimer;
			//카메라 흔들림 메서드 실행
			ShakeCamera(shakePower, shakeDuration);

			fireCooldown = false;
			InstantiateProj();
		}
	}

	private void InstantiateProj()
	{
		GameObject proj = GameManager.Instance.pool.Pop(GameManager.Instance.pool.obj[5].name);
		proj.transform.position = shotPoint.position;
		Vector3 dir = shotDir.position - shotPoint.position;
		//proj.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		proj.gameObject.GetComponent<Rigidbody>().AddForce(dir * fireForce, ForceMode.Impulse);
	}

	private void OnJumpEvent(Context context)
	{
		jumpInput = context.ReadValue<float>();
		//if (frogMove.isGround) jumpCount = 2;
		isJumping = jumpInput != 0;
		if (frogMove.isWater && DataManager.Instance.jumpCount > 1) jumpCount = 1;
		if (isJumping && jumpCount >= 1)
		{
			if (!frogMove.isGround)
			{
				JumpForcing(2f);
			}
			else if (jumpCount > 0)
			{
				JumpForcing(2f);
				frogMove.isGround = false;
			}
		}
	}
	private void JumpForcing(float y)
	{
		Debug.Log("점프진입");
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
