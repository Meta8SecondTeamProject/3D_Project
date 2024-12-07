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

    //������ ������ �߷°��� �̻����� ����
    //�߷��� �����ϰ� Ʈ������, ������ ���� �̿��ؼ� ���� �̵��� �ϰų�
    //�ش� ������Ʈ�� ���ؼ��� �߷� ���� ���� �����ϴ� ���� ����� �˾ƺ��� ��
    //�ִϸ��̼��� �ȴٸ��� ���� ���� ȿ���� �ְ� ������ ����� �����ν�
    //�����ϴ� ��ó�� ���̰� �� ����
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
            //������ �
            Vector3 position = Mathf.Pow(1 - jumpProgress, 2) * startPoint + 2 * (1 - jumpProgress) * jumpProgress * controlPoint + Mathf.Pow(jumpProgress, 2) * endPoint;
            //�� �����Ӹ��� ȣ��� ������ ��ġ�� �����ϹǷ� ���� ����� �浹�Ͽ� ������ ó�� ����.
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
