using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Core
{
    public class WeaponInventory : MonoBehaviour
    {
        [SerializeField] Weapon equippedMeleeWeapon;
        [SerializeField] Weapon equippedRangedWeapon;
        [SerializeField] Weapon activeWeapon;

        public Weapon GetMeleeWeapon()
        {
            return equippedMeleeWeapon;
        }
        public void SetMeleeWeapon(Weapon weapon)
        {
            equippedMeleeWeapon = weapon;
        }

        public Weapon GetRangedWeapon()
        {
            return equippedRangedWeapon;
        }
        public void SetRangedWeapon(Weapon weapon)
        {
            equippedRangedWeapon = weapon;
        }

        public void SetActiveWeapon(Weapon weapon)
        {
            if(activeWeapon == null)
            {
                activeWeapon = equippedMeleeWeapon;
                return;
            }
            else
            {
                activeWeapon = weapon;
            }
        }

        public Weapon GetActiveWeapon()
        {
            return activeWeapon;
        }


    }

}