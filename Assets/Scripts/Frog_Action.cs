using Boxophobic.StyledGUI;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Frog_Action : MonoBehaviour
{
    [Header("머즐(총구)에 임시로 개구리 포지션 넣어주세요")]
    public Transform muzzle;
    public Camera shotPoint;
    public Transform knockbackPos;

    private Rigidbody rb;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    private InputActionAsset controlDefine;
    private InputAction jumpAction;
    private InputAction fireAction;
    private InputAction interaction;

    [Header("반동으로 인한 넉백, 점프, 흔들림")]
    public float knockbackForce;
    public float jumpForce;
    [HideInInspector] public bool isJumping;
    public float shakeDuration;
    public float shakePower;
    //public float shakeOffset;
    private float shakeTimer = 0.5f;

    private Vector3 moveDir;
    private Frog_Move frogMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        controlDefine = GetComponent<PlayerInput>().actions;
        jumpAction = controlDefine.FindAction("Jump");
        fireAction = controlDefine.FindAction("Fire");
        interaction = controlDefine.FindAction("Interaction");
        frogMove = GetComponent<Frog_Move>();
    }

    private void OnEnable()
    {
        fireAction.performed += OnClickEvent;
        fireAction.canceled += OnClickUpEvent;
        jumpAction.performed += OnJumpEvent;
        //interaction.started += OnInteractionEvent;

        isJumping = true;
    }

    //없어도 작동은 하나 불필요한 호출로 인한 메모리 낭비 방지
    private void OnDisable()
    {
        fireAction.performed -= OnClickEvent;
        fireAction.canceled -= OnClickUpEvent;
        jumpAction.performed -= OnJumpEvent;
        //interaction.canceled -= OnInteractionEvent;
    }

    private void Start()
    {
        shakeTimer = shakeDuration;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;

            if (shakeDuration <= 0f && noise != null)
            {
                noise.m_AmplitudeGain = 0f;
                noise.m_FrequencyGain = 0f;
            }
        }
    }

    private void OnClickEvent(InputAction.CallbackContext context)
    {

        //마우스 좌클릭 입력이 감지되면
        if (context.ReadValue<float>() > 0)
        {
            Ray ray = shotPoint.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                Transform hitTarget = hit.transform;

                Vector3 hitPos = hit.point;
                Vector3 knockbackdir = knockbackPos.position - hitPos;
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 2, rb.velocity.z);
                rb.AddForce(knockbackdir.normalized * knockbackForce, ForceMode.Impulse);
                isJumping = true;
                this.shakeDuration = shakeTimer;
                ShakeCamera(shakePower, shakeDuration);
            }
        }
    }

    private void OnClickUpEvent(InputAction.CallbackContext context)
    {
        InputControl control = context.control;

        if (control.name == "rightButton")
        {
            Time.timeScale = 1f;
        }
    }

    private void OnJumpEvent(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<float>());

        if (context.ReadValue<float>() > 0 && frogMove.readyToJump == true)
        {
            //플레이어가 보는 전방 방향 계산
            Vector3 forwardDir = transform.forward;

            //전방 방향과 점프 방향을 더하고
            Vector3 jumpDir = forwardDir + new Vector3(moveDir.x, 0, moveDir.z);

            //정규화 및 Y축 속도 초기화후 점프
            Vector3 normalizedDir = jumpDir.normalized;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(normalizedDir * jumpForce + Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    public void ShakeCamera(float shakePower, float shakeDuration)
    {
        noise.m_PivotOffset = Vector3.one;// * shakeOffset;
        noise.m_AmplitudeGain = shakePower;
        noise.m_FrequencyGain = shakeDuration;
    }
}