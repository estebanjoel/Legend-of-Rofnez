using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public  class poison : MonoBehaviour
{
    [SerializeField] int damageTik = 5;
    [SerializeField] int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerHealth>() != null)
        {
            Health isPoison = other.gameObject.GetComponent<Health>();
            isPoison.poisoned = true;
            isPoison.tikDamage = damageTik;
            isPoison.TakeDamage(10);
        }

    }

}
