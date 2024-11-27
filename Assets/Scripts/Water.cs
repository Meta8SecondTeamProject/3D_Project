using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float power;

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("트리거 호출됨");
        if (other.CompareTag("Player"))
        {
            //Debug.Log("외부 if문 진입");

            if (other.TryGetComponent(out Rigidbody rb))
            {
                //Debug.Log("내부 if문 진입");

                rb.velocity += Vector3.up * power;
                
            }
            
        }
    }
}
