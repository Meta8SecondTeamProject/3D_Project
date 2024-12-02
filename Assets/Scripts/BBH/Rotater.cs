using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [Header("ȸ���� ��")]
    public bool rotateX;
    public bool rotateY;
    public bool rotateZ;
    [Header("ȸ�� �ӵ�")][Tooltip("+ : �ð� ���� \n- : �ݽð� ����")] public float rotateSpeed;
    private void Update()
    {
        //X���� �������� ȸ��
        if(rotateX)
            transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime);

        //Y���� �������� ȸ��
        if(rotateY)
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        
        //Z���� �������� ȸ��
        if(rotateZ)
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
