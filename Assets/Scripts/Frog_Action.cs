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
    private InputActionAsset controlDefine;
    private InputAction jumpAction;
    private InputAction fireAction;
    private InputAction interaction;

    [Header("반동으로 밀려나는 힘, 점프 힘")]
    public float knockbackForce;
    public float jumpForce;
    [HideInInspector] public bool isJumping;

    [Header("우클릭 시간 배율"), Range(0f, 1f)]
    public float timeScale;
    public float fieldOfView = 60f;

    [Header("소지금, 체력, 탄약")]
    public int money;
    public int health;
    public int maxHealth;
    public int currentAmmo;
    [HideInInspector] public int maxAmmo; //추후에 변경할 일이 있을수도 있으니 일단 public

    private Vector3 moveDir;
    private float fireInterval;

    private Frog_Move frogMove;

    private bool zoom;

    private GameObject nearDealer; //가까이 있는 상인 체크용


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
        interaction.started += OnInteractionEvent;

        isJumping = true;
    }

    //없어도 작동은 하나 불필요한 호출로 인한 메모리 낭비 방지
    private void OnDisable()
    {
        fireAction.performed -= OnClickEvent;
        fireAction.canceled -= OnClickUpEvent;
        jumpAction.performed -= OnJumpEvent;
        interaction.canceled -= OnInteractionEvent;
    }

    private void Start()
    {
        maxAmmo = 16; //초기 탄약 맥스값 설정
        maxHealth = 2; //초기 최대 체력 설정
    }

    private void Update()
    {
        Zoom();
    }

    private void OnClickEvent(InputAction.CallbackContext context)
    {
        InputControl control = context.control;

        //마우스 우클릭 입력이 감지되면
        if (control.name == "rightButton" && context.ReadValue<float>() > 0)
        {
            Time.timeScale = timeScale;
            zoom = true;
        }

        //마우스 좌클릭 입력이 감지되면
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
            ////방향 초기화
            //Vector3 jumpDir = transform.TransformDirection(moveDir);
            //Vector3 nomalizedDir = jumpDir.normalized;
            ////Move와 마찬가지로 y축 velocity초기화 후 점프
            //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            ////rb.AddForce(nomalizedDir * jumpForce + Vector3.up * jumpForce, ForceMode.Impulse);
            //rb.AddForce(nomalizedDir.x * jumpForce, nomalizedDir.y * jumpForce, nomalizedDir.z*jumpForce, ForceMode.Impulse);

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

    private void OnInteractionEvent(InputAction.CallbackContext context)
    {
        //OnTriggerEnter로 초기화된 nearDealer의 영향을 받음

        //Debug.Log($"상호작용 이벤트 호출됨, {context.ReadValue<float>()}");

        if (nearDealer == null)
            return;

        if (nearDealer.gameObject.CompareTag("AmmoDealer"))
        {
            BuyAmmo(context.ReadValue<float>());
        }

        if (nearDealer.gameObject.CompareTag("Doctor"))
        {
            HealthRecovery(context.ReadValue<float>());
        }

        if (nearDealer.gameObject.CompareTag("AmmoBeltDealer"))
        {
            BuyAmmoBelt(context.ReadValue<float>());
        }

    }

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("AmmoDealer") || (other.CompareTag("Doctor")))
        {
            nearDealer = other.gameObject;

            if (nearDealer != null)
                Debug.Log($"근처 상인 : {nearDealer.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //상인 초기화
        nearDealer = null;
    }

    private void BuyAmmo(float context)
    {
        //Debug.Log($"BuyAmmo 호출됨, {context}");
        //탄약 상인일 때 F키를 눌러서 입력이 감지되면
        if (nearDealer.CompareTag("AmmoDealer") && context > 0)
        {
            //소지금이 2보다 작거나, 현재 탄약이 최대 탄약 이상이라서 탄약을 사지 못하는 상황이라면
            if (money < 2 && maxAmmo <= currentAmmo)
            {
                return;
                //TODO : 상인 말풍선 출력
            }

            money = money - 2; //TODO : 하드코딩, 탄약상인의 price 및 ammo로 변경 요망
            currentAmmo = currentAmmo + 4;
            //TODO : 상인 말풍선 출력

            //현재 탄약이 최대 탄약수를 넘으면
            if (maxAmmo <= currentAmmo)
            {
                currentAmmo = maxAmmo;
            }
        }
    }

    private void HealthRecovery(float context)
    {
        //Debug.Log($"HealthRecovery 호출됨, {context}");
        //의사일 때 F키를 눌러서 입력이 감지되면
        if (nearDealer.CompareTag("Doctor") && context > 0)
        {
            //체력이 이미 최대체력이라면, 혹시 몰라서 체력이 0이하일때 조건도 추가
            if (health == maxHealth && health <= 0)
            {
                return;
                //TODO : 상인 말풍선 출력
            }
            money = money - 5; 
            health++;
            //TODO : 상인 말풍선 출력 및 개구리 외형 변경
        }
    }

    private void BuyAmmoBelt(float context)
    {

    }
}
//1. 개구리의 후진은 WASD와 관계없음
//2. 개구리가 점프대나 반동으로 인해 점프할 때 S를 누르면 감속은 되나, 
//   Velocity가 0 미만이 되면 더이상 후진이 되지 않음
//3. Frog_Move와 별개로 bool변수 선언해서 Actcion으로 인한 점프인지 
//   판단 후 이동속도 적용해야됨

//4. 개구리가 스페이스바를 누를 때, 반동으로 점프할 때, 점프대를 탔을 때
//   isJumping을 true로 바꾸고, true인동안 Frog_Move에서 감속은 되나 후진은 안되도록
//5. 다시 false로 돌아오는건 CheckSphere 활용

//5-1. CheckSphere의 문제점 : Update에서 돌아가다보니 bool값이 계속 변함
//     한번만 false로 바꾸고, 그 이후에는 상술한 조건을 제외하면 계속 false를 유지하는 로직이 필요함
//     return으로 해결함

//우클릭 했을 때 줌인 및 TimeScale 수정 로직 작성하기, SmoothDamp를 활용하여 부드럽게 줌인되도록 구현
//zoomAction.canceled도 활용하면 bool변수 없이 줌인, 줌아웃 구현 될거같음

//money, ammo는 GameManager가 관리하게 하고
//bool = false -> true -> 