using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Shell : MonoBehaviour
{
    public float addForcePower;
    private Player player;
    private Rigidbody rb;
    public bool isEnemyDrop = false;
    public float moveSpeed;
    public float regenTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameManager.Instance.player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DataManager.Instance.data.ammo += 4;
            UIManager.Instance.GameSceneTextUpdate();
            Destroy(gameObject);
        }
    }

    //�׽�Ʈ�� contextmenu ���� ���� ����
    //���ʹ̰� �׾��� �� �������� �����ϰ� �÷��̾� ���������� ���ư��� �Լ� �߰�
    [ContextMenu("ź�� ���ư���")]
    public void MoveToPlayer()
    {

        Vector3 angle = player.transform.position - transform.position;
        rb.AddForce(angle.normalized * moveSpeed, ForceMode.VelocityChange);


    }

    private void Update()
    {
        if (isEnemyDrop)
        {
            MoveToPlayer();
        }
    }
}