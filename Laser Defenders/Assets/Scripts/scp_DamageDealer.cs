﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    // Start is called before the first frame update

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        //Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
