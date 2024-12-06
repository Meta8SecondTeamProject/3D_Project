using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyCartController : MonoBehaviour
{
    public CinemachineDollyCart dolly;

    private void Start()
    {
        dolly.m_Speed = 10f;
        dolly.m_Position = 10f;
    }
}
