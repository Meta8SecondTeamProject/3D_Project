using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private RawImage crosshair;
    private Color baseColor;
    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        crosshair = GetComponent<RawImage>();
    }

    private void Start()
    {
        if (crosshair != null)
        {
            baseColor = crosshair.color;
        }
    }


    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color = baseColor;
            }
        }
    }
}
