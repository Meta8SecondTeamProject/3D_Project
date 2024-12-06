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
    private float jumpHeight = 30f;
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

    private const float gravity = 9.8f;
    #endregion

    protected override void Start()
    {

        BodyChange();

        isJumping = false;

        base.Start();
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

        rb.velocity = Vector3.zero;

        float jumpTime = Time.time + jumpDuration;

        //rb.AddForce(controlPoint.normalized * speed, ForceMode.Impulse);
        while (jumpTime >= Time.time)
        {
            jumpProgress += Time.deltaTime / jumpDuration;
            //������ �
            Vector3 position = Mathf.Pow(1 - jumpProgress, 2) * startPoint + 2 * (1 - jumpProgress) * jumpProgress * controlPoint + Mathf.Pow(jumpProgress, 2) * endPoint;
            //rb.AddForce(position.normalized * speed, ForceMode.Impulse);
            //rb.position = position;
            Vector3 distance = (position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(distance);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            //transform.LookAt(endPoint);
            rb.MovePosition(position);
            yield return null;
        }

        isJumping = false;
    }

    public float minValue = 2f;
    //������ �ʱ�ȭ
    private void PointInitialization()
    {
        startPoint = transform.position;

        if (target == null)
        {
            target = GameManager.Instance?.player;
        }

        Vector3 targetPos = (target.transform.position - transform.position).normalized;

        endPoint = targetPos * minValue;

        //endPoint = target.transform.position;

        controlPoint = (startPoint + endPoint) / 2 + (Vector3.up * jumpHeight);
        //controlPoint = (startPoint + endPoint) / 2 + (Vector3.up * (Vector3.Distance(transform.position, target.transform.position) / 2));
    }

    //�ٷ� ���ư��� �Ǿ������Ƿ� ������ �ð��� �����Ͽ�
    //���� �������� Ư�� �ð��ȿ� ȸ���� �� �ֵ��� ���� ����
    //����� ���׸� ����Ͽ� ĳ���͸� �ٶ󺸰� �ϸ鼭
    //��ü�� ��ü���� ������Ʈ�� Ʈ�������� �̿��Ͽ�
    //��ü�� �÷��̾��� x,z �ุ �����ϰ� ���� �÷��̾ �ٶ󺸰� ���� ����
    private IEnumerator LookAtPlayer()
    {
        if (target == null)
        {
            Debug.LogError("Toad_Test / LookAtPlayer / target == null");
            target = GameManager.Instance?.player;
        }

        print("LookAtPlayer �ڷ�ƾ ����");
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
        BodyChange();
    }
}
