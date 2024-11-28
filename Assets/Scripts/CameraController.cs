using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    public float moveSpeed = 5f;

    public float rotationSpeed;

    public Transform combatLookAt;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Debug.Log("»£√‚µ ");
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
        orientation.forward = dirToCombatLookAt.normalized;

        playerObj.forward = dirToCombatLookAt.normalized;
        Quaternion targetRotation = Quaternion.LookRotation(viewDir);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
    }
}
