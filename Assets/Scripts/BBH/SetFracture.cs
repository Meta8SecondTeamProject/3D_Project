using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFracture : MonoBehaviour
{
    [SerializeField]public GameObject mesh;
    [SerializeField] public GameObject fracture;

    private Enemy enemy;

    private void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            mesh.SetActive(false);
            fracture.SetActive(true);

        }
    }
}
