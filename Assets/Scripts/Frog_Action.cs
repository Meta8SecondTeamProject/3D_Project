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

    [Header("�ݵ����� �з����� ��, ���� ��")]
    public float knockbackForce;
    public float jumpForce;
    [HideInInspector] public bool isJumping;

    [Header("��Ŭ�� �ð� ����"), Range(0f, 1f)]
    public float timeScale;
    public float fieldOfView = 60f;

    private Vector3 moveDir;
    private float fireInterval;

    private Frog_Move frogMove;

    private bool zoom;
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

        isJumping = true;
    }

    private void OnDisable()
    {
        fireAction.performed -= OnClickEvent;
        fireAction.canceled -= OnClickUpEvent;
        jumpAction.performed -= OnJumpEvent;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (zoom)
        {
            shotPoint.fieldOfView = Mathf.Lerp(shotPoint.fieldOfView, 40f, 0.1f);
        }
        else
        {
            shotPoint.fieldOfView = Mathf.Lerp(shotPoint.fieldOfView, 60f, 0.1f);
        }
        Debug.Log(zoom);
    }

    private void OnClickEvent(InputAction.CallbackContext context)
    {
        InputControl control = context.control;

        //���콺 ��Ŭ�� �Է��� �����Ǹ�
        if (control.name == "rightButton" && context.ReadValue<float>() > 0)
        {
            Time.timeScale = timeScale;
            zoom = true;
        }
        
        //���콺 ��Ŭ�� �Է��� �����Ǹ�
        if (control.name == "leftButton" && context.ReadValue<float>() > 0)
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
            zoom = false;
        }
    }

    private void OnJumpEvent(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<float>());

        if (context.ReadValue<float>() > 0 && frogMove.readyToJump == true)
        {
            ////���� �ʱ�ȭ
            //Vector3 jumpDir = transform.TransformDirection(moveDir);
            //Vector3 nomalizedDir = jumpDir.normalized;
            ////Move�� ���������� y�� velocity�ʱ�ȭ �� ����
            //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            ////rb.AddForce(nomalizedDir * jumpForce + Vector3.up * jumpForce, ForceMode.Impulse);
            //rb.AddForce(nomalizedDir.x * jumpForce, nomalizedDir.y * jumpForce, nomalizedDir.z*jumpForce, ForceMode.Impulse);

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

}
//1. �������� ������ WASD�� �������
//2. �������� �����볪 �ݵ����� ���� ������ �� S�� ������ ������ �ǳ�, 
//   Velocity�� 0 �̸��� �Ǹ� ���̻� ������ ���� ����
//3. Frog_Move�� ������ bool���� �����ؼ� Actcion���� ���� �������� 
//   �Ǵ� �� �̵��ӵ� �����ؾߵ�

//4. �������� �����̽��ٸ� ���� ��, �ݵ����� ������ ��, �����븦 ���� ��
//   isJumping�� true�� �ٲٰ�, true�ε��� Frog_Move���� ������ �ǳ� ������ �ȵǵ���
//5. �ٽ� false�� ���ƿ��°� CheckSphere Ȱ��

//5-1. CheckSphere�� ������ : Update���� ���ư��ٺ��� bool���� ��� ����
//     �ѹ��� false�� �ٲٰ�, �� ���Ŀ��� ����� ������ �����ϸ� ��� false�� �����ϴ� ������ �ʿ���
//     return���� �ذ���

//��Ŭ�� ���� �� ���� �� TimeScale ���� ���� �ۼ��ϱ�, SmoothDamp�� Ȱ���Ͽ� �ε巴�� ���εǵ��� ����
//zoomAction.canceled�� Ȱ���ϸ� bool���� ���� ����, �ܾƿ� ���� �ɰŰ���

//