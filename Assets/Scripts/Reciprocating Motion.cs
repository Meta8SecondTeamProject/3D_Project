using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReciprocatingMotion : MonoBehaviour
{
    [Header("�̵� �ӵ�, �ִ� ���")]
    public float mooveSpeed;
    public float moveAmplitude;

    [Header("�̵��� ��")]
    public bool dirX;
    public bool dirY;
    public bool dirZ;

    private void Update()
    {
        float dir = Mathf.Sin(Time.time * mooveSpeed) * moveAmplitude;

        if(dirX)
            transform.position = new Vector3(transform.position.x + dir, transform.position.y, transform.position.z);

        if (dirY)
            transform.position = new Vector3(transform.position.x, transform.position.y + dir, transform.position.z);

        if (dirZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + dir);
    }
}
