using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //ź��, ����, ü��, ź��

    public bool isMessage = false;
    public bool isInteraction = false;
    protected int price;
    protected int interactionValue;

    public string text;
    private string asdf;

    //UI�� ��� �޼���
    //���� �����ҋ�
    //money<price
    //ü���̳� �Ҹ��� ���� ���� ��




    //������ �÷��̾ ���� �� ��ȣ�ۿ�ǰ�
    //������ ��Ŭ�� ť���
    //��ȣ�ۿ� Ű�� ���� �޽����� ���� ��ȣ�ۿ��� ������ �ٸ���.

    //��ȣ �ۿ�.
    public virtual void Interaction()
    {
        if (isInteraction == false)
        {
            return;
        }
    }
}
