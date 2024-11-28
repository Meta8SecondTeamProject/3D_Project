using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Frog_Action : MonoBehaviour
{
    [Header("����(�ѱ�)�� �ӽ÷� ������ ������ �־��ּ���")]
    public Transform muzzle;
    public Camera shotPoint;
    public Transform knockbackPos;

    private Rigidbody rb;
    private InputActionAsset controlDefine;
    private InputAction jumpAction;
    private InputAction fireAction;
    private InputAction interaction;

    [Header("�ݵ����� ���� �˹�, ����, ��鸲")]
    public float knockbackForce;
    public float jumpForce;
    [HideInInspector] public bool isJumping;

    [Header("��Ŭ�� �ð� ����"), Range(0f, 1f)]
    public float timeScale;
    public float fieldOfView = 60f;

    private Vector3 moveDir;

    private Frog_Move frogMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

    //��� �۵��� �ϳ� ���ʿ��� ȣ��� ���� �޸� ���� ����
    private void OnDisable()
    {
        fireAction.performed -= OnClickEvent;
        fireAction.canceled -= OnClickUpEvent;
        jumpAction.performed -= OnJumpEvent;
        //interaction.canceled -= OnInteractionEvent;
    }

    private void Start()
    {
    }

    private void Update()
    {
        Zoom();
        CameraShake();
    }

    private void OnClickEvent(InputAction.CallbackContext context)
    {

        //���콺 ��Ŭ�� �Է��� �����Ǹ�
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
            //�÷��̾ ���� ���� ���� ���
            Vector3 forwardDir = transform.forward;

            //���� ����� ���� ������ ���ϰ�
            Vector3 jumpDir = forwardDir + new Vector3(moveDir.x, 0, moveDir.z);

            //����ȭ �� Y�� �ӵ� �ʱ�ȭ�� ����
            Vector3 normalizedDir = jumpDir.normalized;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(normalizedDir * jumpForce + Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void Zoom()
    {

    }

    private void CameraShake()
    {

    }
}