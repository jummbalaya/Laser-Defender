using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int healt = 50;

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealler damageDealer = other.GetComponent<DamageDealler>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        healt -= damage;
        if (healt <= 0)
        {
            Destroy(gameObject);
        }
    }
}
