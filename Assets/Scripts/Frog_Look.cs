using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Frog_Look : MonoBehaviour
{
    public Transform cameraPos;
    public Camera mainCamera;

    [Range(5f, 30f)] public float mouseSensivity;
    private float rigAngle = 0f;

    private InputActionAsset controlDefine;
    private InputAction lookAction;

    public LayerMask wallMask;

    private Vector3 defaultCameraOffset = new Vector3(0.5f, 1f, -2f); // �⺻ ī�޶� ��ġ (�÷��̾� ����)

    private void Awake()
    {
        controlDefine = GetComponent<PlayerInput>().actions;
        lookAction = controlDefine.FindAction("Look");
    }

    private void OnEnable()
    {
        lookAction.performed += OnLookEvent;

        //���콺 Ŀ�� �߾����� ���� �� ����� Ȱ��ȭ
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        lookAction.performed -= OnLookEvent;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        //Camera.main.transform.position = cameraPos.position;
    }

    private void FixedUpdate()
    {
        //HandleCameraCollision();
    }

    public void OnLookEvent(InputAction.CallbackContext context)
    {
        Look(context.ReadValue<Vector2>());
    }

    private void Look(Vector2 mouseDelta)
    {
        //ĳ������ �¿� ȸ��
        transform.Rotate(0, mouseDelta.x * mouseSensivity * Time.deltaTime, 0);

        //ĳ���͸� ���� ī�޶�Position�� ���� ȸ��, 
        rigAngle = rigAngle - mouseDelta.y * mouseSensivity * Time.deltaTime;
        rigAngle = Mathf.Clamp(rigAngle, -70f, 100f);
        cameraPos.localEulerAngles = new Vector3(rigAngle, 0, 0);
    }


    private void HandleCameraCollision()
    {
        Vector3 targetCameraPosition = transform.TransformPoint(defaultCameraOffset); // �⺻ ��ġ ���
        Vector3 directionToCamera = targetCameraPosition - transform.position; // �÷��̾� �� ī�޶� ����
        float distanceToCamera = directionToCamera.magnitude;

        // ����׿� �� �׸���
        Debug.DrawLine(transform.position, targetCameraPosition, Color.red, 0.1f);

        if (Physics.Raycast(transform.position, directionToCamera.normalized, out RaycastHit hit, distanceToCamera, wallMask))
        {
            // �浹 �߻�: ī�޶� �浹 �������� �̵�
            Vector3 collisionPosition = hit.point - directionToCamera.normalized * 0.1f; // ������ �ణ ���
            mainCamera.transform.position = collisionPosition;
        }
        else
        {
            // �浹 ����: �⺻ ��ġ�� �̵�
            mainCamera.transform.position = targetCameraPosition;
        }
    }
}


