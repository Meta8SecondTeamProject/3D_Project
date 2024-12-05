using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransform : MonoBehaviour
{
    public Transform[] transforms;
    private Vector3 defaultPos;

    private void OnEnable()
    {
        transforms = GetComponentsInChildren<Transform>();
        
        //foreach (Transform t in transforms)
        //{
        //    defaultPos = t.localPosition;
        //}

        for (int i = 1; i < transforms.Length; i++)
        {
            defaultPos = transforms[i].localPosition;
        }

        Invoke("DisableObject", 5f);
    }

    private void Update()
    {
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        //foreach (Transform t in transforms)
        //{
        //    t.localPosition = defaultPos;
        //}
        for (int i = 1; i < transforms.Length; i++)
        {
            transforms[i].localPosition = defaultPos;
        }
    }
}
