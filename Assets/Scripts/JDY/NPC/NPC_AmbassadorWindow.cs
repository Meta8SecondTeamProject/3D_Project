using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AmbassadorWindow : MonoBehaviour
{
    //�ӽ�
    //�÷��̾��� ��ġ�� ������ �ִ� �̱����� ���� �� �ش� ��ü�� ����
    public GameObject player;
    private void Update()
    {
        LookAtAmbassadorWindow();
    }

    private void LookAtAmbassadorWindow()
    {
        gameObject.transform.LookAt(player.transform.position);
    }
}
