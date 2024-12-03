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


	[Header("마우스 감도")][Range(5f, 30f)] public float mouseSensivity;
	[Header("줌 배율 및 불릿타임 비율")]
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
		//마우스 커서 중앙으로 고정 및 숨기기 활성화
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
		//캐릭터 좌우 회전 (y축 회전)
		float yRotation = mouseDelta.x * mouseSensivity * Time.fixedDeltaTime;
		float xRotation = mouseDelta.y * mouseSensivity * Time.fixedDeltaTime;
		//Quaternion playerRotation = Quaternion.Euler(0, yRotation, 0);
		//rb.MoveRotation(rb.rotation * playerRotation);

		//카메라 상하 회전(x축 회전)
		//rigAngle = new Vector3(rigAngle.x + mouseDelta.y, rigAngle.y + mouseDelta.x, 0);
		//rigAngle.x = Mathf.Clamp(xRotation, -35f, 89f);
		//cameraPos.localRotation = Quaternion.Euler(0, rigAngle.y, rigAngle.x);
	}

	private void Zoom(bool isZoom)
	{
		if (isZoom)
		{
			//Debug.Log("Zoom 활성화");
			Time.timeScale = bulletTimeMag;
			virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, zoomMag, 0.1f);
		}
		else
		{
			//Debug.Log("Zoom 비활성화");
			Time.timeScale = 1f;
			virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, originalZoomMag, 0.1f);
		}
	}




}


