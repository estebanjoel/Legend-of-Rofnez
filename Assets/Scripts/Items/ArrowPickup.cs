using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Item
{
    public class ArrowPickup : ItemPickup
    {
        int arrowQuantity = 0;
        [SerializeField] int arrowMaxQuantity;

        public override void UseItem(GameObject player)
        {
            if(player.GetComponent<WeaponInventory>().GetRangedWeapon() != null)
            {
                arrowQuantity = Random.Range(1, arrowMaxQuantity);
                int currentAmmo = player.GetComponent<RangedWeaponAmmoInventory>().GetAmmo();
                currentAmmo += arrowQuantity;
                player.GetComponent<RangedWeaponAmmoInventory>().SetAmmo(currentAmmo);
            }
            else
            {
                print("No ranged weapon equipped.");
            }
        }
    }

}