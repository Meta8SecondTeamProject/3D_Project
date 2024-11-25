using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;


public class Frog_Move : MonoBehaviour
{
    private Rigidbody rb;
    private InputActionAsset controlDefine;
    private InputAction moveAction;

    [HideInInspector] public Vector2 inputValue; // �Է� ��
    private Vector3 moveDir;
    private Vector3 inputDir;

    [Header("���� üũ��")]
    public Transform groundCheck;
    public LayerMask groundMask;
    private bool grounded;
    public bool readyToJump;

    [Header("�̵� �� ���� ����(3, 0.8)")]
    public float moveSpeed;
    public float jumpForce;

    //[Header("����׿� �ӵ� ǥ�ð�")]
    //public TextMeshProUGUI text;

    private void Awake()
    {
        //������ �ʹ� �ö󰡴ϱ� ��Ʈ�� �߿��� ��¿� �ӽ÷� 60���� ����
        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody>();
        controlDefine = GetComponent<PlayerInput>().actions;
        moveAction = controlDefine.FindAction("Move");
    }

    private void OnEnable()
    {
        moveAction.performed += OnMoveEvent;
        moveAction.canceled += OnMoveEvent;
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMoveEvent;
        moveAction.canceled -= OnMoveEvent;
    }

    private void Update()
    {
        StateHandler();

        //Event���� ȣ���ϴ� WASD�������� ���콺 ���⿡ �°� �ٲ�� ������ �־ Update�� �̵�
        inputDir = new Vector3(inputValue.x, 0, inputValue.y).normalized;
        moveDir = transform.TransformDirection(inputDir) * moveSpeed;

        if (readyToJump==false && inputValue.magnitude > 0)
        {
            Move();
        }
        if (grounded && inputValue.magnitude > 0)
        {
            //TODO : Invoke�� �ϸ� �ǵ��ѰŶ� �ٸ� �������� �߻���, ���� dletaTime�� �������Ѽ� 0.5�ʸ� �����ϴ�, InputSystem�� �ǵ帮�� ���� �ٲٱ�
            Invoke("Jump", 0.5f);
        }

        //Debug.Log(grounded);
        //Debug.Log(inputValue.x); //A : -1 / D : 1
        //Debug.Log(inputValue.y); //W : 1 / S : 1 
        //Debug.Log(inputValue);
        ///text.text = $"current Speed : {(rb.velocity.magnitude * 2.23694f).ToString("F2")}";
    }

    private void OnMoveEvent(InputAction.CallbackContext context)
    {
        //�Է¹��� Ű WASD�� �������� ���� �ʱ�ȭ
        inputValue = context.ReadValue<Vector2>();
    }

    private void Jump()
    {
        if (readyToJump==false)
            return;

        //���� Rigidbody�� y�� �ӵ��� 0���� �ʱ�ȭ�ؼ� ���� ���̰� �̻������� ���� ����
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    private void Move()
    {
        rb.velocity += new Vector3(moveDir.x, 0, moveDir.z) * Time.deltaTime * moveSpeed;
    }

    private void StateHandler()
    {
        //������ �׶����� ������ �ٸ�����
        //0.4f¥�� ���� �״�� ���ڴ� �ٴ��̶� ������ ������ �������� ���ϴ� ���� ������
        //grounded�� 0.05�� �ϸ� �ȵƳ���?
        //�׷� ���������̶� ���� �ٸ��� ������
        grounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);
        readyToJump = Physics.CheckSphere(groundCheck.position, 0.05f, groundMask);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(groundCheck.position, 0.4f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.05f);
    }
}