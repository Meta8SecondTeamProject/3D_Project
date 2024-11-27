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

    private Vector3 defaultCameraOffset = new Vector3(0.5f, 1f, -2f); // 기본 카메라 위치 (플레이어 기준)

    private void Awake()
    {
        controlDefine = GetComponent<PlayerInput>().actions;
        lookAction = controlDefine.FindAction("Look");
    }

    private void OnEnable()
    {
        lookAction.performed += OnLookEvent;

        //마우스 커서 중앙으로 고정 및 숨기기 활성화
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
        //캐릭터의 좌우 회전
        transform.Rotate(0, mouseDelta.x * mouseSensivity * Time.deltaTime, 0);

        //캐릭터를 보는 카메라Position의 상하 회전, 
        rigAngle = rigAngle - mouseDelta.y * mouseSensivity * Time.deltaTime;
        rigAngle = Mathf.Clamp(rigAngle, -70f, 100f);
        cameraPos.localEulerAngles = new Vector3(rigAngle, 0, 0);
    }


    private void HandleCameraCollision()
    {
        Vector3 targetCameraPosition = transform.TransformPoint(defaultCameraOffset); // 기본 위치 계산
        Vector3 directionToCamera = targetCameraPosition - transform.position; // 플레이어 → 카메라 방향
        float distanceToCamera = directionToCamera.magnitude;

        // 디버그용 선 그리기
        Debug.DrawLine(transform.position, targetCameraPosition, Color.red, 0.1f);

        if (Physics.Raycast(transform.position, directionToCamera.normalized, out RaycastHit hit, distanceToCamera, wallMask))
        {
            // 충돌 발생: 카메라를 충돌 지점으로 이동
            Vector3 collisionPosition = hit.point - directionToCamera.normalized * 0.1f; // 벽에서 약간 띄움
            mainCamera.transform.position = collisionPosition;
        }
        else
        {
            // 충돌 없음: 기본 위치로 이동
            mainCamera.transform.position = targetCameraPosition;
        }
    }
}


