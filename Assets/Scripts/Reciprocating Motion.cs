using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReciprocatingMotion : MonoBehaviour
{
    [Header("이동 속도, 최대 운동량")]
    public float mooveSpeed;
    public float moveAmplitude;

    [Header("이동할 축")]
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
