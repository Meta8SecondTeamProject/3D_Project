using System.Collections;
using UnityEngine;

public class King_God_Toad_Test : Boss_Test
{
    #region reference variable
    //hp에 따라 적용할 몸체
    [SerializeField, Tooltip("바디들 0번 : 기본, 1번 : 사망")]
    private GameObject[] bodys;
    //점프할 높이
    [SerializeField, Tooltip("점프할 높이")]
    private float jumpHeight = 10f;
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
        base.Start();

        BodyChange();

        isJumping = false;
    }

    private void BodyChange()
    {
        if (bodys == null) return;

        if (hp > 0)
        {
            bodys[0].SetActive(true);
            bodys[1].SetActive(false);
        }
        else
        {
            bodys[0].SetActive(false);
            bodys[1].SetActive(true);
        }
    }

    protected override IEnumerator WhatName()
    {
        while (hp > 0)
        {
            LookAtPlayer();
            yield return new WaitUntil(() => isJumping);
            JumpToPlayer();
            yield return new WaitWhile(() => isJumping);
        }
    }

    private IEnumerator JumpToPlayer()
    {
        float jumpProgress = 0f;

        PointInitialization();

        float jumpTime = Time.time + jumpDuration;

        while (jumpTime >= Time.time)
        {
            jumpProgress += Time.deltaTime / jumpDuration;
            //배지어 곡선
            Vector3 position = Mathf.Pow(1 - jumpProgress, 2) * startPoint + 2 * (1 - jumpProgress) * jumpProgress * controlPoint + Mathf.Pow(jumpProgress, 2) * endPoint;
            rb.AddForce(position, ForceMode.VelocityChange);
            transform.LookAt(position);
            yield return null;
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

    private IEnumerator LookAtPlayer()
    {
        float delay = Time.time + lookTime;
        while (delay >= Time.time)
        {
            transform.LookAt(target.transform);
            yield return null;
        }
        isJumping = true;
    }

    public override void Die()
    {
        BodyChange();
    }
}
