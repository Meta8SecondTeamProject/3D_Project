using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [Header("�η�, ����")]
    public float buoyancyForce = 30f;
    public float waterDrag = 5f;    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Rigidbody rb))
            {
                Vector3 force = Vector3.up * buoyancyForce;
                rb.AddForce(force, ForceMode.Acceleration);

                //���� ���� ȿ��
                rb.velocity *= 1f - (waterDrag * Time.deltaTime);
            }
        }
    }
}
