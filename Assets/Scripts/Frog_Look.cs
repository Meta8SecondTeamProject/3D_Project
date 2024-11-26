using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Frog_Look : MonoBehaviour
{
    public Transform cameraPos;
    [Range(5f,30f)] public float mouseSensivity;
    private float rigAngle = 0f;

    private InputActionAsset controlDefine;
    private InputAction lookAction;

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

    public void OnLookEvent(InputAction.CallbackContext context)
    {
        Look(context.ReadValue<Vector2>()); 
    }

    private void Look(Vector2 mouseDelta)
    {
        //ĳ������ �¿� ȸ��
        transform.Rotate(0, mouseDelta.x * mouseSensivity * Time.deltaTime, 0);

        //ĳ���͸� ���� ī�޶��� ���� ȸ��, 
        rigAngle = rigAngle - mouseDelta.y * mouseSensivity * Time.deltaTime;
        rigAngle = Mathf.Clamp(rigAngle, -70f, 100f);
        cameraPos.localEulerAngles = new Vector3(rigAngle, 0, 0);
    }
}
