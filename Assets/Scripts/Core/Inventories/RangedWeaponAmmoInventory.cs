using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class RangedWeaponAmmoInventory : MonoBehaviour
    {
        [SerializeField] int ammo;
        int maxAmmo = 99;

        public int GetAmmo()
        {
            return ammo;
        }

        public void SetAmmo(int newAmmo)
        {
            if (newAmmo > maxAmmo) ammo = maxAmmo;
            else ammo = newAmmo;
        }
    }

}