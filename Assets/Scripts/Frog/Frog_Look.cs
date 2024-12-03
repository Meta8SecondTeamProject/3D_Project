using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityTemplateProjects;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class Frog_Look : MonoBehaviour
{
	public Transform cameraPos;
	public Camera mainCamera;
	private CinemachineVirtualCamera virtualCamera;
	public Cinemachine3rdPersonFollow cameraDis;


	[Header("���콺 ����")][Range(5f, 30f)] public float mouseSensivity;
	[Header("�� ���� �� �Ҹ�Ÿ�� ����")]
	[Range(0.1f, 1f)] public float bulletTimeMag;
	[Range(25f, 50f)] public float zoomMag;
	private float originalZoomMag;

	private Vector3 rigAngle;

	private InputActionAsset controlDefine;
	private InputAction lookAction;
	private InputAction zoomAction;
	private Rigidbody rb;

	private bool isZoom;
	private Vector2 lookInput;
	float X;
	float Y;

	private void Awake()
	{
		rb = GetComponentInParent<Rigidbody>();
		controlDefine = GetComponent<PlayerInput>().actions;
		virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
		cameraDis = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
		lookAction = controlDefine.FindAction("Look");
		zoomAction = controlDefine.FindAction("Zoom");
	}

	private void OnEnable()
	{
		//���콺 Ŀ�� �߾����� ���� �� ����� Ȱ��ȭ
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		lookAction.performed += OnLookEvent;
		lookAction.canceled += OnLookEvent;
	}

	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	private void Start()
	{
		originalZoomMag = virtualCamera.m_Lens.FieldOfView;
	}

	private void FixedUpdate()
	{
		//lookInput = lookAction.ReadValue<Vector2>();
		//Look(lookInput);

		isZoom = zoomAction.IsPressed();
		Zoom(isZoom);

	}

	private void OnLookEvent(Context context)
	{
		if (false == SimpleMouseControl.isFocusing) return;
		Look(context.ReadValue<Vector2>());
	}
	private void Look(Vector2 mouseDelta)
	{
		X += mouseDelta.x * mouseSensivity * Time.fixedDeltaTime;
		Y -= mouseDelta.y * mouseSensivity * Time.fixedDeltaTime;
		Y = Mathf.Clamp(Y, -35f, 89f);
		cameraPos.localRotation = Quaternion.Euler(Y, X, 0f);
		//ĳ���� �¿� ȸ�� (y�� ȸ��)
		float yRotation = mouseDelta.x * mouseSensivity * Time.fixedDeltaTime;
		float xRotation = mouseDelta.y * mouseSensivity * Time.fixedDeltaTime;
		//Quaternion playerRotation = Quaternion.Euler(0, yRotation, 0);
		//rb.MoveRotation(rb.rotation * playerRotation);

		//ī�޶� ���� ȸ��(x�� ȸ��)
		//rigAngle = new Vector3(rigAngle.x + mouseDelta.y, rigAngle.y + mouseDelta.x, 0);
		//rigAngle.x = Mathf.Clamp(xRotation, -35f, 89f);
		//cameraPos.localRotation = Quaternion.Euler(0, rigAngle.y, rigAngle.x);
	}

	private void Zoom(bool isZoom)
	{
		if (isZoom)
		{
			//Debug.Log("Zoom Ȱ��ȭ");
			Time.timeScale = bulletTimeMag;
			virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, zoomMag, 0.1f);
		}
		else
		{
			//Debug.Log("Zoom ��Ȱ��ȭ");
			Time.timeScale = 1f;
			virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, originalZoomMag, 0.1f);
		}
	}




}


