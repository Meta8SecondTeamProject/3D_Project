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
        Debug.Log("OnTriggerStay����");
        if (other.CompareTag("Player"))
        {
            Debug.Log("OnTriggerStay �ܺ� if�� ����");

            if (other.TryGetComponent(out Rigidbody rb) && other.TryGetComponent(out Frog_Move move))
            {
                Debug.Log("OnTriggerStay ���� if�� ����");

                move.isWater = true;
                Vector3 force = Vector3.up * buoyancyForce;
                rb.AddForce(force, ForceMode.Acceleration);

                //���� ���� ȿ��
                rb.velocity *= 1f - (waterDrag * Time.deltaTime);
            }
        }
    }
}
