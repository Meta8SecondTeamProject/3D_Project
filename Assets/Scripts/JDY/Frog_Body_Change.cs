using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Body_Change : MonoBehaviour
{
    [Header("��ü�� �ٵ�� �ٵ��� ������ ���缭"), Tooltip("�⺻�ٵ�, ��ó �ٵ�, �ƸӺ�Ʈ���� ��ó, ����, ����, ��Ʈ, �ѱ�")]
    public GameObject[] bodys;

    private void Start()
    {
        bodys[6].SetActive(true);
    }

    [ContextMenu("�ٵ�ü����/�̰Ը³�?")]
    public void BodyChange()
    {
        for (int i = 0; i < bodys.Length - 1; i++)
        {
            bodys[i].SetActive(false);
        }

        if (DataManager.Instance != null)
        {
            if (DataManager.Instance.data.isHat)
            {
                bodys[4].SetActive(true);
            }

            if (DataManager.Instance.data.isAmmoBelt)
            {
                bodys[5].SetActive(true);
                if (DataManager.Instance.data.HP == 1)
                {
                    bodys[2].SetActive(true);
                    return;
                }
            }

            if (DataManager.Instance.data.HP == 1)
            {
                bodys[1].SetActive(true);
                return;
            }
            else if (DataManager.Instance.data.HP == 2)
            {
                bodys[0].SetActive(true);
                return;
            }
            else
            {
                bodys[3].SetActive(true);
                return;
            }
        }


    }
}