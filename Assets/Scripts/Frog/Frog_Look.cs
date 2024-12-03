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
	private Rigidbody rb;

	private bool isZoom;
	private Vector2 lookInput;
	private void Awake()
	{
		rb = GetComponentInParent<Rigidbody>();
		controlDefine = GetComponent<PlayerInput>().actions;
		brain = GetComponentInChildren<CinemachineBrain>();
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
		//originalZoomMag = freeLookCam.m_Lens.FieldOfView;

	}

	private void FixedUpdate()
	{
		//lookInput = lookAction.ReadValue<Vector2>();
		//Look(lookInput);

		//isZoom = zoomAction.IsPressed();
		//Zoom(isZoom);


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
	//		//Debug.Log("Zoom 활성화");
	//		Time.timeScale = bulletTimeMag;
	//		virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, zoomMag, 0.1f);
	//	}
	//	else
	//	{
	//		//Debug.Log("Zoom 비활성화");
	//		Time.timeScale = 1f;
	//		virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, originalZoomMag, 0.1f);
	//	}
	//}




}


