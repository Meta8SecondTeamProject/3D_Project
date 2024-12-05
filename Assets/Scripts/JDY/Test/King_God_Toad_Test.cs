using System.Collections;
using UnityEngine;

public class King_God_Toad_Test : Boss_Test
{
    #region reference variable
    //hp�� ���� ������ ��ü
    [SerializeField, Tooltip("�ٵ�� 0�� : �⺻, 1�� : ���")]
    private GameObject[] bodys;
    //������ ����
    [SerializeField, Tooltip("������ ����")]
    private float jumpHeight = 10f;
    //�ٶ� �ð�
    [SerializeField, Tooltip("�÷��̾ �ٶ� �ð�")]
    private float lookTime = 5f;
    //������ �ð�
    [SerializeField, Tooltip("���߿� �ӹ��� �ð�")]
    private float jumpDuration = 1f;
    #endregion

    #region local variable
    //������ ��� �̿��� �����͵�
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
            //������ �
            Vector3 position = Mathf.Pow(1 - jumpProgress, 2) * startPoint + 2 * (1 - jumpProgress) * jumpProgress * controlPoint + Mathf.Pow(jumpProgress, 2) * endPoint;
            rb.AddForce(position, ForceMode.VelocityChange);
            transform.LookAt(position);
            yield return null;
        }

        isJumping = false;
    }

    //������ �ʱ�ȭ
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
