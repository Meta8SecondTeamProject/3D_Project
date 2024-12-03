using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Die_Test : MonoBehaviour
{
    public Rigidbody[] rigidbodys;
    public float explosionForce = 5000f;
    public float explosionRadius = 5f;
    public Vector3 explosionOffset = Vector3.up;

    public float delay = 5f;

    private void Start()
    {
        TriggerDeathEffect();
    }


    [ContextMenu("Test/사지분해")]
    public void TriggerDeathEffect()
    {
        Vector3 explosionCenter = transform.position + explosionOffset;

        foreach (Rigidbody rigidbody in rigidbodys)
        {
            if (rigidbody == null) continue;

            rigidbody.isKinematic = false;

            rigidbody.useGravity = true;

            rigidbody.AddExplosionForce(explosionForce, explosionCenter, explosionRadius);

            StartCoroutine(DisableObject(rigidbody.gameObject));
        }
    }

    private IEnumerator DisableObject(GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
