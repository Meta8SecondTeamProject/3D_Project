using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Test : MonoBehaviour
{
    //ź��, ����, ü��, ź��

    protected GameManager gameManager;
    protected bool message;
    protected bool interaction;

    protected virtual void Start()
    {
        if (gameManager != null)
        {
            gameManager = GameManager.Instance;
        }
    }

    //������ �÷��̾ ���� �� ��ȣ�ۿ�ǰ�
    //������ ��Ŭ�� ť���
    //��ȣ�ۿ� Ű�� ���� �޽����� ���� ��ȣ�ۿ��� ������ �ٸ���.

    //��ȣ �ۿ�.
    public virtual void Interaction()
    {

    }

}
