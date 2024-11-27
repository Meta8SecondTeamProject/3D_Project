using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform player;

    [Range(50f, 500f)]
    public float sensitivity = 200f;

    private CinemachineComposer composer;

    void Start()
    {
        composer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();

        if (composer == null)
        {
            Debug.LogError("CinemachineComposer not found on Virtual Camera.");
        }
    }

    void Update()
    {
        Debug.Log($"Main Camera Position: {Camera.main.transform.position}");
        if (composer == null) return;

        // ���콺 �Է�
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // �÷��̾� �¿� ȸ��
        player.Rotate(Vector3.up * mouseX);

        // ī�޶� ���� ȸ��
        composer.m_TrackedObjectOffset.y += mouseY;
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -2f, 2f); // ���� ����
    }
}

