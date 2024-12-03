using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : MonoBehaviour
{
    [SerializeField] private float forwardForce = 10f;
    [SerializeField] private float upForce = 40f;

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log($"Collision °¨ÁöµÊ : {other.gameObject.name}");

        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log($"Player °¨ÁöµÊ : {other.gameObject.name}");

            if (other.gameObject.TryGetComponent(out Rigidbody rb))
            {
                //rb.velocity=new Vector3(rb.velocity.x,0,rb.velocity.z);
                rb.velocity = new Vector3(rb.velocity.x, upForce, rb.velocity.z) + (rb.transform.right * forwardForce);
                //rb.AddForce(rb.transform.up * upForce, ForceMode.VelocityChange);
            }
        }
    }
}
