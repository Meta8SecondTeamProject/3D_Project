using System.Collections;
using UnityEngine;


//���� �߷� �� ������ y������ -20���� �Ǿ� �־�
//������ٵ��� �߷��� true�� ������ �ڷ� �з����� ������ ����
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

        print("RigidBody" + rb.name);
        print(rb);
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
        print("�ڷ�ƾ Ȱ��ȭ");
        while (hp > 0)
        {
            print("�ڷ�ƾ �ݺ��� ����");
            StartCoroutine(LookAtPlayer());
            yield return new WaitUntil(() => isJumping);
            StartCoroutine(JumpToPlayer());
            yield return new WaitWhile(() => isJumping);
        }
    }

    //������ ������ �߷°��� �̻����� ����
    //�߷��� �����ϰ� Ʈ������, ������ ���� �̿��ؼ� ���� �̵��� �ϰų�
    //�ش� ������Ʈ�� ���ؼ��� �߷� ���� ���� �����ϴ� ���� ����� �˾ƺ��� ��
    //�ִϸ��̼��� �ȴٸ��� ���� ���� ȿ���� �ְ� ������ ����� �����ν�
    //�����ϴ� ��ó�� ���̰� �� ����
    private IEnumerator JumpToPlayer()
    {
        print("JumpToPlayer �ڷ�ƾ ����");
        float jumpProgress = 0f;

        PointInitialization();

        float jumpTime = Time.time + jumpDuration;

        while (jumpTime >= Time.time)
        {
            jumpProgress += Time.deltaTime / jumpDuration;
            //������ �
            Vector3 position = Mathf.Pow(1 - jumpProgress, 2) * startPoint + 2 * (1 - jumpProgress) * jumpProgress * controlPoint + Mathf.Pow(jumpProgress, 2) * endPoint;
            //rb.AddForce(position * Time.fixedDeltaTime);
            rb.position = position;
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

    //�ٷ� ���ư��� �Ǿ������Ƿ� ������ �ð��� �����Ͽ�
    //���� �������� Ư�� �ð��ȿ� ȸ���� �� �ֵ��� ���� ����
    //����� ���׸� ����Ͽ� ĳ���͸� �ٶ󺸰� �ϸ鼭
    //��ü�� ��ü���� ������Ʈ�� Ʈ�������� �̿��Ͽ�
    //��ü�� �÷��̾��� x,z �ุ �����ϰ� ���� �÷��̾ �ٶ󺸰� ���� ����
    private IEnumerator LookAtPlayer()
    {
        print("LookAtPlayer �ڷ�ƾ ����");
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
