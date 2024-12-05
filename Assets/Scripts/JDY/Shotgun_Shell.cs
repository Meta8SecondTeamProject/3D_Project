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

    //테스트용 contextmenu 추후 삭제 무관
    //에너미가 죽었을 때 랜덤으로 생성하고 플레이어 방향쪽으로 날아가게 함수 추가
    [ContextMenu("탄약 날아가요")]
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