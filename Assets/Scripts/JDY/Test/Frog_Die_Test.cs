using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Die_Test : MonoBehaviour
{
    private Vector3[] pos;
    public Rigidbody[] rigidbodys;
    public float explosionForce = 5000f;
    public float explosionRadius = 5f;
    public Vector3 explosionOffset = Vector3.up;

    public float delay = 5f;

    private void OnEnable()
    {
        pos = new Vector3[rigidbodys.Length];
        if (rigidbodys.Length != pos.Length)
        {
            Debug.LogError("Frog_Die_Test / OnEnable");
            return;
        }
        for (int i = 0; i < rigidbodys.Length; i++)
        {
            pos[i] = rigidbodys[i].position;
        }
    }

    private void OnDisable()
    {
        if (rigidbodys.Length != pos.Length)
        {
            Debug.LogError("Frog_Die_Test / OnDisable");
        }
        for (int i = 0; i < rigidbodys.Length; i++)
        {
            rigidbodys[i].position = pos[i];
        }
    }

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
