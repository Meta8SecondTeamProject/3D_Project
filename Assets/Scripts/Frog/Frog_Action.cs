using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class Frog_Action : MonoBehaviour
{
	[Header("머즐(총구)에 임시로 개구리 포지션 넣어주세요")]
	public Transform muzzle;
	public Camera shotPoint;
	public Transform knockbackPos;

	private Rigidbody rb;
	private CinemachineVirtualCamera virtualCamera;
	private CinemachineBasicMultiChannelPerlin noise;
	private InputActionAsset controlDefine;
	private InputAction jumpAction;
	private InputAction fireAction;

	[Header("반동으로 인한 넉백, 점프, 흔들림")]
	public float knockbackForce;
	public float jumpForce;
	[HideInInspector] public bool isJumping;
	public float shakeDuration;
	public float shakePower;
	//public float shakeOffset;
	private float shakeTimer = 0.5f;

	private Vector3 moveDir;
	private Frog_Move frogMove;
	public LayerMask groundMask;

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
		jumpAction.performed += OnJumpEvent;

		isJumping = true;
	}

	//없어도 작동은 하나 불필요한 호출로 인한 메모리 낭비 방지
	private void OnDisable()
	{
		fireAction.performed -= OnClickEvent;
		jumpAction.performed -= OnJumpEvent;
	}

	private void Start()
	{
		shakeTimer = shakeDuration;
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
		if (context.ReadValue<float>() > 0)
		{
			Ray ray = shotPoint.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000f))
			{
				Transform hitTarget = hit.transform;

				Debug.Log(hitTarget.transform.position);

				//레이가 명중한곳의 좌표와
				Vector3 hitPos = hit.point;

				//개구리 뒤통수에 붙은 반동용 포지션과 계산하여 방향을 구함
				//반동 포지션은 새로 수정 예정
				//Vector3 knockbackdir = knockbackPos.position - hitPos;
				Vector3 knockbackdir = knockbackPos.position - muzzle.position;


				//반동으로 튀어오를때 떨어지는 속도가 너무 빠르면 제대로 튀어오르지 못하므로 Y축 속도만 제어
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 2, rb.velocity.z);

				//방향대로 AddForce메서드 실행
				//반동 포지션은 새로 수정 예정
				rb.AddForce(knockbackdir * knockbackForce, ForceMode.Impulse);

				//카메라 흔들림 변수 초기화
				shakeDuration = shakeTimer;

				//카메라 흔들림 메서드 실행
				ShakeCamera(shakePower, shakeDuration);
			}
		}
	}

	private void OnJumpEvent(Context context)
	{
		Debug.Log(context.ReadValue<float>());

		if (context.ReadValue<float>() > 0 && frogMove.isGround == true)
		{
			//플레이어가 보는 전방 방향 계산
			Vector3 forwardDir = transform.forward;

			//전방 방향과 점프 방향을 더하고
			Vector3 jumpDir = forwardDir + new Vector3(moveDir.x, 0, moveDir.z);

			//정규화 및 Y축 속도 초기화후 점프
			Vector3 normalizedDir = jumpDir.normalized;
			rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
			rb.AddForce(normalizedDir * jumpForce + Vector3.up * jumpForce, ForceMode.Impulse);
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