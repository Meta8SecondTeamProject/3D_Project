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


	[Header("마우스 감도")][Range(5f, 30f)] public float mouseSensivity;
	[Header("줌 배율 및 불릿타임 비율")]
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
	public float zoomValue;

	private Vector2 lookInput;
	private float escapeInput;
	public bool isSetting = false;



	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		controlDefine = GetComponent<PlayerInput>().actions;
		brain = GetComponentInChildren<CinemachineBrain>();
		lookAction = controlDefine.FindAction("Look");
		zoomAction = controlDefine.FindAction("Zoom");
		escapeAction = controlDefine.FindAction("ESC");
		//야매
		cameraNearPos.localRotation = Quaternion.Euler(0, -90, 0);
	}

	private void OnEnable()
	{
		lookAction.performed += OnLookEvent;
		lookAction.canceled += OnLookEvent;

		zoomAction.performed += OnZoomEvent;
		zoomAction.canceled += OnZoomEvent;

		escapeAction.performed += OnEscapeEvent;
	}

	private void OnDisable()
	{
		lookAction.performed -= OnLookEvent;
		lookAction.canceled -= OnLookEvent;

		zoomAction.performed -= OnZoomEvent;
		zoomAction.canceled -= OnZoomEvent;

		escapeAction.performed -= OnEscapeEvent;
	}



	private void Start()
	{
		//StartCoroutine(SettingMenu());
		originalZoomMag = freeLookCam.m_Lens.FieldOfView;

    }

	private void Update()
	{
		Zoom(isZoom);
	}



	private void OnZoomEvent(Context context)
	{
		zoomValue = context.ReadValue<float>();
		isZoom = zoomValue != 0;
	}

	private void OnEscapeEvent(Context context)
	{
		escapeInput = context.ReadValue<float>();
		isSetting = escapeInput != 0;
		UIManager.Instance.gameMenuController.SettingMenuOnOff();
		if (UIManager.Instance.gameMenuController.pausedMenu.activeSelf == false)
		{
			Cursor.lockState = CursorLockMode.Locked;
			GameManager.Instance.player.frogAction.canFire = true;
		}
		else if (UIManager.Instance.gameMenuController.pausedMenu.activeSelf)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	private void OnLookEvent(Context context)
	{
		Look(context.ReadValue<Vector2>());
	}
	private void Look(Vector2 mouseDelta)
	{
		float yRotation = mouseDelta.x * mouseSensivity * Time.fixedDeltaTime;
		Quaternion playerRotation = Quaternion.Euler(0, yRotation, 0);
		rb.MoveRotation(rb.rotation * playerRotation);
		rigAngle.x -= mouseDelta.y * (mouseSensivity * 0.01f);
		if (rigAngle.x <= 0)
		{
			//Debug.Log("1");
			freeLookCam.gameObject.SetActive(false);
			rigAngle.x = Mathf.Clamp(rigAngle.x, -40f, 35f);
			cameraNearPos.localRotation = Quaternion.Euler(rigAngle.x, -90, 0);
		}
		else
		{
			//Debug.Log("2");
			freeLookCam.gameObject.SetActive(true);
			rigAngle.x = Mathf.Clamp(rigAngle.x, -15f, 89f);
			cameraPos.localRotation = Quaternion.Euler(rigAngle.x, -90, 0);
		}

	}

	private void Zoom(bool isZoom)
	{
		if (isZoom)
		{
			//Debug.Log("Zoom 활성화");
			Time.timeScale = bulletTimeMag;
			freeLookCam.m_Lens.FieldOfView = Mathf.Lerp(freeLookCam.m_Lens.FieldOfView, zoomMag, 0.1f);
			nearLookCam.m_Lens.FieldOfView = Mathf.Lerp(nearLookCam.m_Lens.FieldOfView, zoomMag, 0.1f);
		}
		else
		{
			//Debug.Log("Zoom 비활성화");
			Time.timeScale = 1f;
			freeLookCam.m_Lens.FieldOfView = Mathf.Lerp(freeLookCam.m_Lens.FieldOfView, originalZoomMag, 0.1f);
			nearLookCam.m_Lens.FieldOfView = Mathf.Lerp(nearLookCam.m_Lens.FieldOfView, originalZoomMag, 0.1f);
		}
	}




}


