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
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

    public void OnLookEvent(InputAction.CallbackContext context)
    {
        //Look(context.ReadValue<Vector2>());
    }

    private void Update()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        Look(lookInput);
    }

    private void Look(Vector2 mouseDelta)
    {
        // ĳ���� �¿� ȸ�� (y�� ȸ��)
        float yRotation = mouseDelta.x * mouseSensivity * Time.deltaTime;
        Quaternion playerRotation = Quaternion.Euler(0, yRotation, 0);
        rb.MoveRotation(rb.rotation * playerRotation);

        // ī�޶� ���� ȸ�� (x�� ȸ��)
        rigAngle -= mouseDelta.y * mouseSensivity * Time.deltaTime;
        rigAngle = Mathf.Clamp(rigAngle, -70f, 90f);
        cameraPos.localRotation = Quaternion.Euler(rigAngle, 0, 0);
    }

}


