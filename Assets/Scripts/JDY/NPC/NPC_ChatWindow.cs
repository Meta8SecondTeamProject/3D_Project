using UnityEngine;

public class NPC_ChatWindow : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = GameManager.Instance?.player;
    }
    private void Update()
    {
        WindowLookCam();
    }

    private void WindowLookCam()
    {
        if (player == null)
        {
            player = GameManager.Instance?.player;
            return;
        }
        gameObject.transform.LookAt(player.frogLook.mainCamera.transform.position);
    }
}
