using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Spawn : NPC
{
    private bool isBoos;

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
            Instantiate(ambassadorWindow).transform.position = transform.position + (Vector3.up * 10f);
        }
    }

    private void OnDisable()
    {
        isBoos = false;
    }


}
