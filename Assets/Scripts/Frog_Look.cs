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
    private CinemachineVirtualCamera virtualCamera;
    //public Cinemachine3rdPersonFollow cameraDis;

    [Header("���콺 ����")][Range(5f, 30f)] public float mouseSensivity;
    [Header("�� ���� �� �Ҹ�Ÿ�� ����")]
    [Range(0.1f, 1f)] public float bulletTimeMag;
    [Range(1f, 2.5f)] public float zoomMag;
    private float originalZoomMag;
    public float lenght;

    private float rigAngle = 0f;

    private InputActionAsset controlDefine;
    private InputAction lookAction;
    private InputAction zoomAction;
    private Rigidbody rb;

    private bool isZoom;
    private Vector2 lookInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controlDefine = GetComponent<PlayerInput>().actions;
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        //cameraDis = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        lookAction = controlDefine.FindAction("Look");
        zoomAction = controlDefine.FindAction("Zoom");
        lenght = virtualCamera.m_Lens.FieldOfView;
    }

    private void OnEnable()
    {
        //lookAction.performed += OnLookEvent;
        //zoomAction.performed += OnZoomDownEvent;
        //zoomAction.canceled += OnZoomUpEvent;

        //���콺 Ŀ�� �߾����� ���� �� ����� Ȱ��ȭ
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        //lookAction.performed -= OnLookEvent;
        //zoomAction.performed -= OnZoomDownEvent;
        //zoomAction.canceled -= OnZoomUpEvent;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        //if (cameraDis != null)
        //    Debug.Log(cameraDis.CameraDistance);
        //else
        //    Debug.Log("�̰� Null�̸� �� ���� ��� �ϳ�");

        //originalZoomMag = cameraDis.CameraDistance;

        Debug.Log(lenght);
    }

    private void Update()
    {
        lookInput = lookAction.ReadValue<Vector2>();
        Look(lookInput);

        isZoom = zoomAction.IsPressed();
        Zoom(isZoom);

        Debug.Log(zoomMag);
    }

    private void Look(Vector2 mouseDelta)
    {
        //ĳ���� �¿� ȸ�� (y�� ȸ��)
        float yRotation = mouseDelta.x * mouseSensivity * Time.deltaTime;
        Quaternion playerRotation = Quaternion.Euler(0, yRotation, 0);
        rb.MoveRotation(rb.rotation * playerRotation);

        //ī�޶� ���� ȸ�� (x�� ȸ��)
        rigAngle -= mouseDelta.y * mouseSensivity * Time.deltaTime;
        rigAngle = Mathf.Clamp(rigAngle, -70f, 90f);
        cameraPos.localRotation = Quaternion.Euler(rigAngle, 0, 0);
    }

    private void Zoom(bool isZoom)
    {
        if (isZoom)
        {
            Debug.Log("Zoom Ȱ��ȭ");
            Time.timeScale = bulletTimeMag;
            virtualCamera.m_Lens.FieldOfView = 40f;
        }
        else
        {
            Debug.Log("Zoom ��Ȱ��ȭ");
            Time.timeScale = 1f;
            virtualCamera.m_Lens.FieldOfView = 60f;
        }
        
    }

}


