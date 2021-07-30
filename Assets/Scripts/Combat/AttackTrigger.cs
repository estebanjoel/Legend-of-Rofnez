using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class AttackTrigger : MonoBehaviour
    {
        [SerializeField] GameObject weaponCollider;
        [SerializeField] float triggerDamage;


        public void ActivateWeaponCollider()
        {
            weaponCollider.SetActive(true);
        }

        public void DeactivateWeaponCollider()
        {
            weaponCollider.SetActive(false);
        }

        public void SetTriggerDamage(float damage)
        {
            triggerDamage = damage;
        }

        public float GetTriggerDamage()
        {
            return triggerDamage;
        }
    }
}

