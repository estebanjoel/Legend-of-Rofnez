using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Item
{
    public class ShieldPickup : ItemPickup
    {
        [SerializeField] Shield shieldToEquip;
        
        public override void UseItem(GameObject player)
        {
            player.GetComponent<Fighter>().EquipShield(shieldToEquip);
        }
    }

}

