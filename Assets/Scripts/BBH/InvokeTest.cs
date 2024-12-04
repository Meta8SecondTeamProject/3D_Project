using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeTest : MonoBehaviour
{
    void Start()
    {
        Invoke("Test", 1f);
    }
    
    private void Test()
    {
        Debug.Log("Hello World");
    }
}
