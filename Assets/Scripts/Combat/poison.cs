using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class poison : MonoBehaviour
    {
        [SerializeField] int damageTik = 5;
        [SerializeField] float damage = 2.5f;
        [SerializeField] GameObject poisonVFX;

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerHealth>() != null)
            {
                Health player = other.GetComponent<Health>();
                getPoisoned(player);   
            }
        }

        public virtual void getPoisoned(Health isPoison)
        {
            if (!isPoison.poisoned)
            {
                isPoison.poisonDamages(damageTik, damage);
                isPoison.SpawnShader(poisonVFX);
            }
        }

    }
}
