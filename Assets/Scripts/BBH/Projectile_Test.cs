using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Test : MonoBehaviour
{
    public GameObject ProjectileTemplate;
    public float ProjectileSpeed = 10.0f;
    private Enemy enemy;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire(Camera.main.ScreenPointToRay(Input.mousePosition));
        }
    }

    private void Fire(Ray ray)
    {
        GameObject cannonBall = Instantiate(ProjectileTemplate, ray.origin + ray.direction.normalized * 0.1f, Quaternion.identity);
        cannonBall.GetComponent<Rigidbody>().velocity = ray.direction.normalized * ProjectileSpeed;
    }
}

