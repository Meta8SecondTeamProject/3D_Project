using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //ź��, ����, ü��, ź��

    protected bool message;
    protected bool interaction;


    //������ �÷��̾ ���� �� ��ȣ�ۿ�ǰ�
    //������ ��Ŭ�� ť���
    //��ȣ�ۿ� Ű�� ���� �޽����� ���� ��ȣ�ۿ��� ������ �ٸ���.

    //��ȣ �ۿ�.
    public virtual void Interaction() { }


}
