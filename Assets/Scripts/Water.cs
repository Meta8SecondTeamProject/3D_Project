using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float power;

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Ʈ���� ȣ���");
        if (other.CompareTag("Player"))
        {
            //Debug.Log("�ܺ� if�� ����");

            if (other.TryGetComponent(out Rigidbody rb))
            {
                //Debug.Log("���� if�� ����");

                rb.velocity += Vector3.up * power;
                
            }
            
        }
    }
}
