using System.Collections;
using UnityEngine;


//현재 중력 값 설정이 y축으로 -20으로 되어 있어
//리지드바디의 중력을 true로 설정시 뒤로 밀려나는 현상이 있음
public class King_God_Toad_Test : Boss_Test
{
    #region reference variable
    //hp에 따라 적용할 몸체
    [SerializeField, Tooltip("바디들 0번 : 기본, 1번 : 사망")]
    private GameObject[] bodys;
    //점프할 높이
    [SerializeField, Tooltip("점프할 높이")]
    private float jumpHeight = 30f;
    //바라볼 시간
    [SerializeField, Tooltip("플레이어를 바라볼 시간")]
    private float lookTime = 5f;
    //점프할 시간
    [SerializeField, Tooltip("공중에 머무를 시간")]
    private float jumpDuration = 1f;
    #endregion

    #region local variable
    //배지어 곡선을 이용할 포인터들
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 controlPoint;

    private bool isJumping;
    #endregion

    protected override void Start()
    {
        BodyChange();
        isJumping = false;
        base.Start();
    }

    private void BodyChange()
    {
        if (bodys == null) return;

        bodys[0].SetActive(hp > 0);
        bodys[1].SetActive(hp <= 0);
    }

    protected override IEnumerator WhatName()
    {
        while (hp > 0)
        {
            StartCoroutine(LookAtPlayer());
            yield return new WaitUntil(() => isJumping);
            StartCoroutine(JumpToPlayer());
            yield return new WaitWhile(() => isJumping);
        }
    }

    //점프를 하지만 중력값의 이상으로 인해
    //중력을 제거하고 트랜스폼, 포지션 등을 이용해서 직접 이동을 하거나
    //해당 오브젝트에 관해서만 중력 값을 따로 설정하는 등의 방법을 알아봐야 함
    //애니메이션을 팔다리만 뻗는 등의 효과를 주고 정면을 곡선으로 줌으로써
    //점프하는 것처럼 보이게 할 예정
    private IEnumerator JumpToPlayer()
    {
        float jumpProgress = 0f;
        PointInitialization();
        rb.velocity = Vector3.zero;
        float jumpTime = Time.time + jumpDuration;

        //rb.AddForce(controlPoint.normalized * speed, ForceMode.Impulse);
        while (jumpTime >= Time.time)
        {
            jumpProgress += Time.deltaTime / jumpDuration;
            //배지어 곡선
            Vector3 position = Mathf.Pow(1 - jumpProgress, 2) * startPoint + 2 * (1 - jumpProgress) * jumpProgress * controlPoint + Mathf.Pow(jumpProgress, 2) * endPoint;
            //매 프레임마다 호출시 강제로 위치를 변경하므로 물리 연산과 충돌하여 버벅임 처럼 보임.
            //rb.MovePosition(position);

            Vector3 velocity = (position - rb.position) / Time.fixedDeltaTime;

            rb.velocity = velocity;

            Vector3 distance = (position - transform.position).normalized;
            if (distance.sqrMagnitude > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation(distance);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            }
            yield return new WaitForFixedUpdate();
        }

        isJumping = false;
    }

    //포인터 초기화
    private void PointInitialization()
    {
        startPoint = transform.position;

        if (target == null)
        {
            target = GameManager.Instance?.player;
        }

        endPoint = target.transform.position;

        controlPoint = (startPoint + endPoint) / 2 + (Vector3.up * jumpHeight);
    }

    //바로 돌아가게 되어있으므로 보간과 시간을 설정하여
    //일정 방향으로 특정 시간안에 회전할 수 있도록 설정 예정
    //헤드쪽 리그를 사용하여 캐릭터를 바라보게 하면서
    //몸체는 전체적인 오브젝트의 트랜스폼을 이용하여
    //몸체는 플레이어의 x,z 축만 설정하고 헤드는 플레이어를 바라보게 설정 예정
    private IEnumerator LookAtPlayer()
    {
        if (target == null)
        {
            Debug.LogError("Toad_Test / LookAtPlayer / target == null");
            target = GameManager.Instance?.player;
        }

        float delay = Time.time + lookTime;
        while (delay >= Time.time)
        {
            Vector3 distance = (target.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(distance.x, 0f, distance.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            //transform.LookAt(target.transform);
            yield return null;
        }
        isJumping = true;
    }

    public override void Die()
    {
        DataManager.Instance.EndGame();
        BodyChange();
    }
}
