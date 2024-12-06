using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Shell : MonoBehaviour
{
    private Player player;
    private Rigidbody rb;
    public bool isEnemyDrop = false;
    public float moveSpeed;
    public ObjectPool pool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameManager.Instance.player;
        pool = GameManager.Instance.pool;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DataManager.Instance.data.ammo += 4;
            UIManager.Instance.GameSceneTextUpdate();
            if (isEnemyDrop)
            {
                pool.Push(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

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