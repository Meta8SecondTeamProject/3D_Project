using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Frog_Action : MonoBehaviour
{
    public Transform muzzle;
    public Camera shotPoint;

    private Rigidbody rb;
    private InputActionAsset controlDefine;
    private InputAction jumpAction;
    private InputAction fireAction;
    private InputAction interaction;
    [Header("7, 5")]
    public float knockbackForce;
    public float jumpForce;

    private Vector3 moveDir;
    private float fireInterval;

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
        fireAction.performed += OnFireEvent;
        jumpAction.performed += OnJumpEvent;
    }

    private void OnDisable()
    {

    }

    private void Update()
    {

    }

    private void OnFireEvent(InputAction.CallbackContext context)
    {
        InputControl control = context.control;

        //���콺 ��Ŭ�� �Է��� �����Ǹ�
        if (control.name == "leftButton" && context.ReadValue<float>() > 0)
        {
            Ray ray = shotPoint.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                Transform hitTarget = hit.transform;

                //if (hitTarget.gameObject.CompareTag("Enemy")) 
                //{
                    Vector3 hitPos = hit.point;
                    Vector3 knockbackdir = transform.position - hitPos;
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 2, rb.velocity.z); 
                    rb.AddForce(knockbackdir.normalized * knockbackForce, ForceMode.Impulse);
                //}
            }
        }

        //Debug.Log(context.ReadValue<float>());
        //Debug.Log(control.name); //leftButton, rightButton ��� Ȯ�ε�
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
        }
    }
}