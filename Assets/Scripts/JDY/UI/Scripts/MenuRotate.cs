using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotate : MonoBehaviour
{
    public float set;
    public float speed = 2f;
    private const float maxAngle = 3.5f;

    private void Update()
    {
        float angle = Mathf.Sin((set + 1) * speed * Time.time) * maxAngle;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
