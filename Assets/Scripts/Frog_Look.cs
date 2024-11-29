using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Frog_Look : MonoBehaviour
{
	public Transform cameraPos;
	public Camera mainCamera;
	private CinemachineVirtualCamera camera;
	//public Cinemachine3rdPersonFollow cameraDis;

	[Header("마우스 감도")][Range(5f, 30f)] public float mouseSensivity;
	[Header("줌 배율 및 불릿타임 비율")]
	[Range(0.1f, 1f)] public float bulletTimeMag;
	[Range(25f, 50f)] public float zoomMag;
	private float originalZoomMag;

	private float rigAngle = 0f;

	private InputActionAsset controlDefine;
	private InputAction lookAction;
	private InputAction zoomAction;
	private Rigidbody rb;

	private bool isZoom;
	private Vector2 lookInput;

	private void Awake()
	{
		rb = GetComponentInParent<Rigidbody>();
		//controlDefine = GetComponent<PlayerInput>().actions;
		camera = GetComponentInChildren<CinemachineVirtualCamera>();
		//cameraDis = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
		//lookAction = controlDefine.FindAction("Look");
		//zoomAction = controlDefine.FindAction("Zoom");
	}

	private void OnEnable()
	{
		//마우스 커서 중앙으로 고정 및 숨기기 활성화
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	private void Start()
	{


		originalZoomMag = camera.m_Lens.FieldOfView;


	}

	private void Update()
	{
		//lookInput = lookAction.ReadValue<Vector2>();
		//Look(lookInput);

		//isZoom = zoomAction.IsPressed();
		//Zoom(isZoom);
	}

	private void Look(Vector2 mouseDelta)
	{
		//캐릭터 좌우 회전 (y축 회전)
		float yRotation = mouseDelta.x * mouseSensivity * Time.deltaTime;
		Quaternion playerRotation = Quaternion.Euler(0, yRotation, 0);
		rb.MoveRotation(rb.rotation * playerRotation);

		//카메라 상하 회전(x축 회전)
		rigAngle -= mouseDelta.y * mouseSensivity * Time.deltaTime;
		rigAngle = Mathf.Clamp(rigAngle, -70f, 90f);
		cameraPos.localRotation = Quaternion.Euler(rigAngle, 0, 0);
	}

	private void Zoom(bool isZoom)
	{
		if (isZoom)
		{
			//Debug.Log("Zoom 활성화");
			Time.timeScale = bulletTimeMag;
			camera.m_Lens.FieldOfView = Mathf.Lerp(camera.m_Lens.FieldOfView, zoomMag, 0.05f);
		}
		else
		{
			//Debug.Log("Zoom 비활성화");
			Time.timeScale = 1f;
			camera.m_Lens.FieldOfView = Mathf.Lerp(camera.m_Lens.FieldOfView, originalZoomMag, 0.05f);
		}
	}
}


