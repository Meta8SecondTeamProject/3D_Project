using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Boss_Spawn : NPC
{
    private bool isBoos;
    public GameObject spawnPos;


    private void OnEnable()
    {
        isBoos = true;
    }

    public override void Interaction()
    {
        base.Interaction();

        if (isBoos)
        {
            isBoos = false;
            Instantiate(ambassadorWindow).transform.position = spawnPos.transform.position;
        }
    }

    private void OnDisable()
    {
        isBoos = false;
    }


}
