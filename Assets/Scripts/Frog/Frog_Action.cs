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

	[Header("반동으로 인한 넉백, 점프, 흔들림")]
	[Range(0, 5)]
	public float knockbackForce;
	public float jumpForce;
	public float shakeDuration;
	public float shakePower;
	//public float shakeOffset;
	private float shakeTimer = 0.5f;

	private float jumpInput;
	public bool isJumping;
	private bool fireCooldown;

	private Vector3 moveDir;
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
		frogMove = GetComponent<Frog_Move>();
	}

	private void OnEnable()
	{
		fireAction.performed += OnClickEvent;
		fireAction.canceled += OnClickEvent;
		jumpAction.performed += OnJumpEvent;
		jumpAction.canceled += OnJumpEvent;

	}

	//없어도 작동은 하나 불필요한 호출로 인한 메모리 낭비 방지
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
			//Ray ray = new Ray(crossHair.transform.position, crossHair.transform.forward);
			//Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 0.2f);
			yield return new WaitWhile(() => fireCooldown);
			yield return new WaitForSeconds(0.5f);
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

	private void OnClickEvent(Context context)
	{
		Debug.Log("클릭 감지");
		//마우스 좌클릭 입력이 감지되면
		Debug.Log($"Fireinput : {fireCooldown}");
		if (context.ReadValue<float>() > 0 && fireCooldown)
		{
			particle.Play(true);
			//Ray ray = shotPoint.ScreenPointToRay(Input.mousePosition);
			//RaycastHit hit;
			//if (Physics.Raycast(ray, out hit, 1000f))
			//{
			//	Transform hitTarget = hit.transform;

			//	Debug.Log(hitTarget.transform.position);

			//레이가 명중한곳의 좌표와
			//Vector3 hitPos = hit.point;

			//개구리 뒤통수에 붙은 반동용 포지션과 계산하여 방향을 구함
			//반동 포지션은 새로 수정 예정
			//Vector3 knockbackdir = knockbackPos.position - hitPos;
			Vector3 knockbackdir = knockbackPos.position - muzzlePos.position;


			//반동으로 튀어오를때 떨어지는 속도가 너무 빠르면 제대로 튀어오르지 못하므로 Y축 속도만 제어
			//rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 2, rb.velocity.z);

			//방향대로 AddForce메서드 실행
			//반동 포지션은 새로 수정 예정
			rb.AddForce(knockbackdir * knockbackForce, ForceMode.VelocityChange);

			//카메라 흔들림 변수 초기화
			shakeDuration = shakeTimer;

			//카메라 흔들림 메서드 실행
			ShakeCamera(shakePower, shakeDuration);
			fireCooldown = false;
		}

	}


	private void OnJumpEvent(Context context)
	{

		jumpInput = context.ReadValue<float>();
		Debug.Log(jumpInput);
		isJumping = jumpInput != 0;
		frogMove.isMove = false;
		if (isJumping && frogMove.isGround == true)
		{
			Debug.Log("점프진입");
			Vector3 forwardDir = jumpDir.position - transform.position;
			rb.AddForce(forwardDir.normalized * jumpForce, ForceMode.Impulse);
		}
	}

	private void ShakeCamera(float shakePower, float shakeDuration)
	{
		noise.m_PivotOffset = Vector3.one;// * shakeOffset;
		noise.m_AmplitudeGain = shakePower;
		noise.m_FrequencyGain = shakeDuration;
	}
}

//1. Frog_Move 에서 Grounded가 true일때 앞쪽으로 AddForce하도록 하고, 뒤 제한값 수정하기
//2. 반동 로직 수정하기, 왜 고장났는지 파악하기
//3. 마우스 눌렀을 때 투사체 발사 기능 추가