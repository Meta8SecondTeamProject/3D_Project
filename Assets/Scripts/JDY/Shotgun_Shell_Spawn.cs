using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Shell_Spawn : MonoBehaviour
{
    [SerializeField]
    private Shotgun_Shell shell;
    [SerializeField]
    private float spawnTime;
    private float currentSpawnTime;
    private void Update()
    {
        ShellSpawn();
    }

    private void ShellSpawn()
    {
        if (shell.gameObject.activeSelf == false)
        {
            if (currentSpawnTime == 0f)
            {
                currentSpawnTime = Time.time + spawnTime;
            }

            if (Time.time >= currentSpawnTime)
            {
                shell.gameObject.SetActive(true);
                shell.gameObject.transform.localPosition = Vector3.zero;
                currentSpawnTime = 0f;
            }
        }
    }

}

