using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class Frog_Action : MonoBehaviour
{
	[Header("����(�ѱ�)�� �ӽ÷� ������ ������ �־��ּ���")]
	public Transform muzzlePos;
	public Camera shotPoint;
	public Transform knockbackPos;
	public Transform jumpDir;

	private Rigidbody rb;
	private CinemachineVirtualCamera virtualCamera;
	private CinemachineBasicMultiChannelPerlin noise;
	private InputActionAsset controlDefine;
	private InputAction jumpAction;
	private InputAction fireAction;

	[Header("�ݵ����� ���� �˹�, ����, ��鸲")]
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

	//��� �۵��� �ϳ� ���ʿ��� ȣ��� ���� �޸� ���� ����
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
			yield return new WaitForSeconds(0.5f);
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

	private void OnClickEvent(Context context)
	{
		Debug.Log("Ŭ�� ����");
		//���콺 ��Ŭ�� �Է��� �����Ǹ�
		Debug.Log($"input : {fireCooldown}");
		if (context.ReadValue<float>() > 0 && fireCooldown)
		{
			//Ray ray = shotPoint.ScreenPointToRay(Input.mousePosition);
			//RaycastHit hit;
			//if (Physics.Raycast(ray, out hit, 1000f))
			//{
			//	Transform hitTarget = hit.transform;

			//	Debug.Log(hitTarget.transform.position);

			//���̰� �����Ѱ��� ��ǥ��
			//Vector3 hitPos = hit.point;

			//������ ������� ���� �ݵ��� �����ǰ� ����Ͽ� ������ ����
			//�ݵ� �������� ���� ���� ����
			//Vector3 knockbackdir = knockbackPos.position - hitPos;
			Vector3 knockbackdir = knockbackPos.position - muzzlePos.position;


			//�ݵ����� Ƣ������� �������� �ӵ��� �ʹ� ������ ����� Ƣ������� ���ϹǷ� Y�� �ӵ��� ����
			//rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 2, rb.velocity.z);

			//������ AddForce�޼��� ����
			//�ݵ� �������� ���� ���� ����
			rb.AddForce(knockbackdir * knockbackForce, ForceMode.VelocityChange);

			//ī�޶� ��鸲 ���� �ʱ�ȭ
			shakeDuration = shakeTimer;

			//ī�޶� ��鸲 �޼��� ����
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
			Debug.Log("��������");
			//�÷��̾ ���� ���� ���� ���
			Vector3 forwardDir = jumpDir.position - transform.position;
			rb.AddForce(forwardDir * jumpForce, ForceMode.Impulse);
			//���� ����� ���� ������ ���ϰ�
			//Vector3 jumpDir = forwardDir + new Vector3(moveDir.x, 0, moveDir.z);

			////����ȭ �� Y�� �ӵ� �ʱ�ȭ�� ����
			//Vector3 normalizedDir = jumpDir.normalized;
			//rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
			//rb.AddForce(normalizedDir * jumpForce + Vector3.up * jumpForce, ForceMode.Impulse);
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