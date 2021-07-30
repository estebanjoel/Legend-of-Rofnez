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
        Weapon activeWeapon;


        public void SetMeleeWeapon(Weapon weapon)
        {
            equippedMeleeWeapon = weapon;
        }

        public void SetRangedWeapon(Weapon weapon)
        {
            equippedRangedWeapon = weapon;
        }

        public void SetActiveWeapon()
        {
            if(activeWeapon == null)
            {
                activeWeapon = equippedMeleeWeapon;
                return;
            }
            else
            {
                if (activeWeapon == equippedMeleeWeapon && equippedRangedWeapon != null) activeWeapon = equippedRangedWeapon;
                else if (activeWeapon == equippedRangedWeapon) activeWeapon = equippedMeleeWeapon;
            }
        }

        public Weapon GetActiveWeapon()
        {
            return activeWeapon;
        }


    }

}