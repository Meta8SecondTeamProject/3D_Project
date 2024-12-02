using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class Frog_Action : MonoBehaviour
{
	[Header("����(�ѱ�)�� �ӽ÷� ������ ������ �־��ּ���")]
	public Transform muzzle;
	public Camera shotPoint;
	public Transform knockbackPos;

	private Rigidbody rb;
	private CinemachineVirtualCamera virtualCamera;
	private CinemachineBasicMultiChannelPerlin noise;
	private InputActionAsset controlDefine;
	private InputAction jumpAction;
	private InputAction fireAction;

	[Header("�ݵ����� ���� �˹�, ����, ��鸲")]
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

	//��� �۵��� �ϳ� ���ʿ��� ȣ��� ���� �޸� ���� ����
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

	private void OnClickEvent(Context context)
	{
		Debug.Log("Ŭ�� ����");
		//���콺 ��Ŭ�� �Է��� �����Ǹ�
		if (context.ReadValue<float>() > 0)
		{
			Ray ray = shotPoint.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000f))
			{
				Transform hitTarget = hit.transform;

				Debug.Log(hitTarget.transform.position);

				//���̰� �����Ѱ��� ��ǥ��
				Vector3 hitPos = hit.point;

				//������ ������� ���� �ݵ��� �����ǰ� ����Ͽ� ������ ����
				//�ݵ� �������� ���� ���� ����
				//Vector3 knockbackdir = knockbackPos.position - hitPos;
				Vector3 knockbackdir = knockbackPos.position - muzzle.position;


				//�ݵ����� Ƣ������� �������� �ӵ��� �ʹ� ������ ����� Ƣ������� ���ϹǷ� Y�� �ӵ��� ����
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 2, rb.velocity.z);

				//������ AddForce�޼��� ����
				//�ݵ� �������� ���� ���� ����
				rb.AddForce(knockbackdir * knockbackForce, ForceMode.Impulse);

				//ī�޶� ��鸲 ���� �ʱ�ȭ
				shakeDuration = shakeTimer;

				//ī�޶� ��鸲 �޼��� ����
				ShakeCamera(shakePower, shakeDuration);
			}
		}
	}

	private void OnJumpEvent(Context context)
	{
		Debug.Log(context.ReadValue<float>());

		if (context.ReadValue<float>() > 0 && frogMove.isGround == true)
		{
			//�÷��̾ ���� ���� ���� ���
			Vector3 forwardDir = transform.forward;

			//���� ����� ���� ������ ���ϰ�
			Vector3 jumpDir = forwardDir + new Vector3(moveDir.x, 0, moveDir.z);

			//����ȭ �� Y�� �ӵ� �ʱ�ȭ�� ����
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

//1. Frog_Move ���� Grounded�� true�϶� �������� AddForce�ϵ��� �ϰ�, �� ���Ѱ� �����ϱ�
//2. �ݵ� ���� �����ϱ�, �� ���峵���� �ľ��ϱ�
//3. ���콺 ������ �� ����ü �߻� ��� �߰�