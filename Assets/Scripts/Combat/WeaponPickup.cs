using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Item;

namespace RPG.Combat
{
    public class WeaponPickup : ItemPickup
    {
        [SerializeField] Weapon weaponToEquip = null;

        public override void UseItem(GameObject player)
        {
            player.GetComponent<Fighter>().EquipWeapon(weaponToEquip);
        }
    }
}
