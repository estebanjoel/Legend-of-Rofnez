using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class RangedWeaponAmmoInventory : MonoBehaviour
    {
        [SerializeField] int ammo;

        public int GetAmmo()
        {
            return ammo;
        }

        public void SetAmmo(int newAmmo)
        {
            ammo = newAmmo;
        }
    }

}