using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(Rigidbody))]
public class Boss_Test : MonoBehaviour
{
    protected int hp;
    public int MaxHp { private get; set; }

    private Rigidbody rb;
    public Player target;
    public float speed;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WhatName());
    }

    private void OnEnable()
    {
        if (MaxHp <= 0)
        {
            hp = 10;
        }
        else
        {
            hp = MaxHp;
        }

        if (GameManager.Instance.player != null)
        {
            target = GameManager.Instance.player;
        }
    }

    public virtual void Hit()
    {
        hp -= 1;
        if (hp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //죽는 애니메이션
        gameObject.SetActive(false);
    }

    protected virtual IEnumerator WhatName()
    {
        yield return null;
    }
}
