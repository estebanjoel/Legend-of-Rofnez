﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Item
{
    public class HealPickup : ItemPickup
    {
        [SerializeField] float pointToHeal;
        public override void UseItem(GameObject player)
        {
            player.GetComponent<Health>().Heal(pointToHeal);
            player.GetComponent<Animator>().SetTrigger("Heal");
        }
    }
}
