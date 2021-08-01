using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;
using RPG.Core;

namespace RPG.Combat
{
    public class WeaponPickup : ItemPickup
    {
        [SerializeField] Weapon weaponToEquip = null;

        public override void UseItem(GameObject player)
        {
            WeaponInventory playerWeaponInventory = player.GetComponent<WeaponInventory>();
            
            if(weaponToEquip.HasProjectile())
            {
                player.GetComponent<RangedWeaponAmmoInventory>().SetAmmo(weaponToEquip.GetAmmo());
                playerWeaponInventory.SetRangedWeapon(weaponToEquip);
                playerWeaponInventory.SetActiveWeapon(playerWeaponInventory.GetRangedWeapon());
            }

            else
            {
                playerWeaponInventory.SetMeleeWeapon(weaponToEquip);
                playerWeaponInventory.SetActiveWeapon(playerWeaponInventory.GetMeleeWeapon());
            }

            player.GetComponent<Fighter>().EquipWeapon(weaponToEquip);
        }
    }
}
