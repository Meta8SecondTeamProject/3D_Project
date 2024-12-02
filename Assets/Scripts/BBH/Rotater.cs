using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [Header("회전할 축")]
    public bool rotateX;
    public bool rotateY;
    public bool rotateZ;
    [Header("회전 속도")][Tooltip("+ : 시계 방향 \n- : 반시계 방향")] public float rotateSpeed;
    private void Update()
    {
        //X축을 기준으로 회전
        if(rotateX)
            transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime);

        //Y축을 기준으로 회전
        if(rotateY)
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        
        //Z축을 기준으로 회전
        if(rotateZ)
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
