using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCulling : MonoBehaviour
{
    [SerializeField, Header("�ø��� �Ÿ�")]
    private float cullingDis = 100f;

    private Transform player;
    //�ø��� ������Ʈ�� ����Ʈ�� ����, �±׷δ� ����
    [TextArea(1, 2)]
    public string explanation = "�ø��� ������Ʈ �±׸� Culling���� �ٲ��ּ���.\n���� �����ʴ� ���� ������Ʈ�� �����մϴ�.";
    public List<GameObject> cullingObj = new List<GameObject>();

    private void Start()
    {
        //�÷��̾ �Ź� �����ϴ� �����̹Ƿ� �̷��� �ʱ�ȭ���ϸ� null��Ű���
        //�Ƹ�? ��?����
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("�÷��̾ ã���� ���� (Distance Culling)");

        //���� �ε�Ǹ� GameObject�� ��� ã�ƿ�
        GameObject[] objects = Resources.FindObjectsOfTypeAll<GameObject>();

        //�� �߿��� Culling�±װ� ���� ������Ʈ�� ����Ʈ�� ����
        foreach (GameObject obj in objects)
        {
            if (obj.CompareTag("Culling"))
            {
                cullingObj.Add(obj);
            }
        }
    }

    //�÷��̾�� Culling �±װ� ���� ������Ʈ�� ã�ƿͼ� �÷��̾���� �Ÿ��� ���� ��Ȱ��ȭ ó����
    private void Update()
    {
        //null üũ
        if (player == null)
            return;

        //�±׷� ã�°� ������, �� �����Ӹ��� Find�� ȣ���ϴ°� ���ɿ� ������ �ʰ�,
        //Find�� ��Ȱ��ȭ�� ������Ʈ�� ã�� ���ϹǷ� �ٽ� Ȱ��ȭ�ǰ� �Ϸ��� �ٸ� ������ �ʿ���
        foreach (GameObject obj in cullingObj)//GameObject.FindGameObjectsWithTag("Culling"))
        {
            float distance = Vector3.Distance(player.transform.position, obj.transform.position);
            Debug.Log($"�Ÿ� : {distance}");
            Debug.Log($"�÷��̾� �̸� : {player.name}");
            Debug.Log($"������Ʈ �̸� : {obj.name}");
            obj.SetActive(distance <= cullingDis);                
        }
    }
}

