using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{

    public GameObject[] triggerOns;
    public GameObject[] triggerOffs;
    private void Awake()
    {
        TriggerOnOff(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            TriggerOnOff(true);
        }
    }

    private void TriggerOnOff(bool setBool)
    {
        foreach (var triggerOff in triggerOffs)
        {
            triggerOff.SetActive(!setBool);
        }
        foreach (var triggerOn in triggerOns)
        {
            triggerOn.SetActive(setBool);
        }
    }
}
