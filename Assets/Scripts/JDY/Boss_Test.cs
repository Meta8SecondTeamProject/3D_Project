using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class Boss_Test : MonoBehaviour
{
    private int hp;
    public int MaxHp { private get; set; }
    public State state;

    private Rigidbody rb;
    public Transform target;
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        hp = MaxHp;
        state = State.LookAtMe;
    }

    private void Update()
    {
        SetState();
    }

    public virtual void Hit()
    {
        hp -= 1;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void SetState()
    {
        switch (state)
        {
            case State.LookAtMe:
                LookAtMeState();
                break;
            case State.Idle:
                IdleState();
                break;
            default:
                Debug.LogError("Boss_Test / SetState / Error");
                break;
        }
    }

    protected virtual void LookAtMeState()
    {
        Vector3 movePos = target.position - transform.position;

        rb.AddForce(movePos * speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    protected virtual void IdleState()
    {

    }

    public virtual void Die()
    {
        //죽는 애니메이션
        gameObject.SetActive(false);
    }


}

//상태 머신 필요한가???
//에너미는 플레이어 쪽으로 돌진만 하는데;;;;
public enum State
{
    Idle,
    LookAtMe
}
