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
    public float shakeAmount;

    private float shakeTime;
    private Vector3 cameraShakePos;
    [HideInInspector] public bool isJumping;

    [Header("��Ŭ�� �ð� ����"), Range(0f, 1f)]
    public float timeScale;
    public float fieldOfView = 60f;

    [Header("������, ü��, ź��")]
    public int money;
    public int health;
    public int maxHealth;
    public int currentAmmo;
    [HideInInspector] public int maxAmmo; //���Ŀ� ������ ���� �������� ������ �ϴ� public

    private Vector3 moveDir;
    private float fireInterval;

    private Frog_Move frogMove;

    private bool zoom;

    private GameObject nearDealer; //������ �ִ� ���� üũ��


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
        maxAmmo = 16; //�ʱ� ź�� �ƽ��� ����
        maxHealth = 2; //�ʱ� �ִ� ü�� ����

        cameraShakePos = shotPoint.transform.position;
    }

    private void Update()
    {
        Zoom();
        CameraShake();
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
                shakeTime = 0.5f;
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

    //private void OnInteractionEvent(InputAction.CallbackContext context)
    //{
    //    //OnTriggerEnter�� �ʱ�ȭ�� nearDealer�� ������ ����

    //    //Debug.Log($"��ȣ�ۿ� �̺�Ʈ ȣ���, {context.ReadValue<float>()}");

    //    if (nearDealer == null)
    //        return;

    //    if (nearDealer.gameObject.CompareTag("AmmoDealer"))
    //    {
    //        BuyAmmo(context.ReadValue<float>());
    //    }

    //    if (nearDealer.gameObject.CompareTag("Doctor"))
    //    {
    //        HealthRecovery(context.ReadValue<float>());
    //    }

    //    if (nearDealer.gameObject.CompareTag("AmmoBeltDealer"))
    //    {
    //        BuyAmmoBelt(context.ReadValue<float>());
    //    }

    //}

    private void Zoom()
    {
        if (zoom)
        {
            shotPoint.fieldOfView = Mathf.Lerp(shotPoint.fieldOfView, 40f, 0.1f);
        }
        else
        {
            shotPoint.fieldOfView = Mathf.Lerp(shotPoint.fieldOfView, 60f, 0.1f);
        }
    }

    private void CameraShake()
    {
        if (shakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * shakeAmount + cameraShakePos;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0;
            transform.position = cameraShakePos;
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("AmmoDealer") || (other.CompareTag("Doctor")))
    //    {
    //        nearDealer = other.gameObject;

    //        if (nearDealer != null)
    //            Debug.Log($"��ó ���� : {nearDealer.name}");
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    //���� �ʱ�ȭ
    //    nearDealer = null;
    //}

    //private void BuyAmmo(float context)
    //{
    //    //Debug.Log($"BuyAmmo ȣ���, {context}");
    //    //ź�� ������ �� FŰ�� ������ �Է��� �����Ǹ�
    //    if (nearDealer.CompareTag("AmmoDealer") && context > 0)
    //    {
    //        //�������� 2���� �۰ų�, ���� ź���� �ִ� ź�� �̻��̶� ź���� ���� ���ϴ� ��Ȳ�̶��
    //        if (money < 2 && maxAmmo <= currentAmmo)
    //        {
    //            return;
    //            //TODO : ���� ��ǳ�� ���
    //        }

    //        money = money - 2; //TODO : �ϵ��ڵ�, ź������� price �� ammo�� ���� ���
    //        currentAmmo = currentAmmo + 4;
    //        //TODO : ���� ��ǳ�� ���

    //        //���� ź���� �ִ� ź����� ������
    //        if (maxAmmo <= currentAmmo)
    //        {
    //            currentAmmo = maxAmmo;
    //        }
    //    }
    //}

    //private void HealthRecovery(float context)
    //{
    //    //Debug.Log($"HealthRecovery ȣ���, {context}");
    //    //�ǻ��� �� FŰ�� ������ �Է��� �����Ǹ�
    //    if (nearDealer.CompareTag("Doctor") && context > 0)
    //    {
    //        //ü���� �̹� �ִ�ü���̶��, Ȥ�� ���� ü���� 0�����϶� ���ǵ� �߰�
    //        if (health == maxHealth && health <= 0)
    //        {
    //            return;
    //            //TODO : ���� ��ǳ�� ���
    //        }
    //        money = money - 5;
    //        health++;
    //        //TODO : ���� ��ǳ�� ��� �� ������ ���� ����
    //    }
    //}

    //private void BuyAmmoBelt(float context)
    //{

    //}
}