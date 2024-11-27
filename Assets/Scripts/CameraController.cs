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

        // 마우스 입력
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // 플레이어 좌우 회전
        player.Rotate(Vector3.up * mouseX);

        // 카메라 상하 회전
        composer.m_TrackedObjectOffset.y += mouseY;
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -2f, 2f); // 각도 제한
    }
}

