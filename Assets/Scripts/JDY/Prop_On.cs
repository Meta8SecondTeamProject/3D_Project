using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Prop_On : MonoBehaviour
{
    private const float movePos = 33f;
    private Vector3 arrivePos;
    [SerializeField]
    private TextMeshPro[] texts;
    private int data;

    private void Start()
    {
        arrivePos = transform.position + (Vector3.up * movePos);
        data = DataManager.Instance.birdKillCount;
        foreach (var text in texts)
        {
            text.text = $"���̴� {data}\n ����";
        }
    }

    //�ӽ�
    //DataManager���� ���� ���� �� ������ �Լ��� ȣ���ϴ� ����� ���� ���̴µ�...
    private void Update()
    {
        if (DataManager.Instance.birdKillCount != data)
        {
            data = DataManager.Instance.birdKillCount;
            foreach (var text in texts)
            {
                text.text = $"���̴� {data}\n ����";
            }
        }
    }

    public IEnumerator PropOn()
    {
        while (Vector3.Distance(transform.position, arrivePos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, arrivePos, 0.5f * Time.unscaledDeltaTime);
            yield return null;
        }
        transform.position = arrivePos;
    }

    //�۵��ϴ��� Ȯ�ο� ���� ���� ����
    [ContextMenu("�ܵ��� ������")]
    public void Test()
    {
        while (Vector3.Distance(transform.position, arrivePos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, arrivePos, 0.5f * Time.unscaledDeltaTime);
        }
        transform.position = arrivePos;
    }
}
