using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [Header("부력, 저항")]
    public float buoyancyForce = 30f;
    public float waterDrag = 5f;    

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay진입");
        if (other.CompareTag("Player"))
        {
            Debug.Log("OnTriggerStay 외부 if문 진입");

            if (other.TryGetComponent(out Rigidbody rb) && other.TryGetComponent(out Frog_Move move))
            {
                Debug.Log("OnTriggerStay 내부 if문 진입");

                move.isWater = true;
                Vector3 force = Vector3.up * buoyancyForce;
                rb.AddForce(force, ForceMode.Acceleration);

                //물의 저항 효과
                rb.velocity *= 1f - (waterDrag * Time.deltaTime);
            }
        }
    }
}
