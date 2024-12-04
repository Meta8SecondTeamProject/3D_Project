using Cinemachine;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityTemplateProjects;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class Frog_Look : MonoBehaviour
{
	public Transform cameraPos;
	public Transform cameraNearPos;
	public Camera mainCamera;
	public CinemachineVirtualCamera freeLookCam;
	public CinemachineVirtualCamera nearLookCam;
	private CinemachineBrain brain;


	[Header("���콺 ����")][Range(5f, 30f)] public float mouseSensivity;
	[Header("�� ���� �� �Ҹ�Ÿ�� ����")]
	[Range(0.1f, 1f)] public float bulletTimeMag;
	[Range(25f, 50f)] public float zoomMag;
	private float originalZoomMag;

	private Vector3 rigAngle;

	private InputActionAsset controlDefine;
	private InputAction lookAction;
	private InputAction zoomAction;
	private InputAction escapeAction;
	private Rigidbody rb;

	private bool isZoom;
	private Vector2 lookInput;
	private float escapeInput;
	private bool escapeDown;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		controlDefine = GetComponent<PlayerInput>().actions;
		brain = GetComponentInChildren<CinemachineBrain>();
		lookAction = controlDefine.FindAction("Look");
		zoomAction = controlDefine.FindAction("Zoom");
		escapeAction = controlDefine.FindAction("ESC");
		//�߸�
		cameraNearPos.localRotation = Quaternion.Euler(0, -90, 0);
	}

	private void OnEnable()
	{
		lookAction.performed += OnLookEvent;
		lookAction.canceled += OnLookEvent;
		//TODO : �� �߰�����

		escapeAction.performed += OnEscapeEvent;
	}

	private void OnDisable()
	{
		lookAction.performed -= OnLookEvent;
		lookAction.canceled -= OnLookEvent;
		//TODO : �� �߰�����

		escapeAction.performed -= OnEscapeEvent;
	}

	private void OnEscapeEvent(Context context)
	{
		escapeInput = context.ReadValue<float>();
		escapeDown = escapeInput != 0;
		Debug.Log("OnEscape");
		if (escapeDown)
			CursorManager.Instance.CursorChange();
	}
	private void Start()
	{
		//originalZoomMag = freeLookCam.m_Lens.FieldOfView;
	}

	private void FixedUpdate()
	{
		//lookInput = lookAction.ReadValue<Vector2>();
		//Look(lookInput);

		//isZoom = zoomAction.IsPressed();
		//Zoom(isZoom);
		//Debug.Log($"���콺 ���� {Cursor.lockState}");
	}

	private void OnLookEvent(Context context)
	{
		if (false == SimpleMouseControl.isFocusing) return;
		Look(context.ReadValue<Vector2>());
	}
	private void Look(Vector2 mouseDelta)
	{
		float yRotation = mouseDelta.x * mouseSensivity * Time.fixedDeltaTime;
		Quaternion playerRotation = Quaternion.Euler(0, yRotation, 0);
		rb.MoveRotation(rb.rotation * playerRotation);
		rigAngle.x -= mouseDelta.y * (mouseSensivity * 0.01f);
		if (rigAngle.x < 0)
		{
			freeLookCam.gameObject.SetActive(false);
			rigAngle.x = Mathf.Clamp(rigAngle.x, -40f, 89f);
			cameraNearPos.localRotation = Quaternion.Euler(rigAngle.x, -90, 0);
		}
		else
		{
			freeLookCam.gameObject.SetActive(true);
			rigAngle.x = Mathf.Clamp(rigAngle.x, -15f, 89f);
			cameraPos.localRotation = Quaternion.Euler(rigAngle.x, -90, 0);
		}

	}

	//private void Zoom(bool isZoom)
	//{
	//	if (isZoom)
	//	{
	//		//Debug.Log("Zoom Ȱ��ȭ");
	//		Time.timeScale = bulletTimeMag;
	//		virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, zoomMag, 0.1f);
	//	}
	//	else
	//	{
	//		//Debug.Log("Zoom ��Ȱ��ȭ");
	//		Time.timeScale = 1f;
	//		virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, originalZoomMag, 0.1f);
	//	}
	//}




}


