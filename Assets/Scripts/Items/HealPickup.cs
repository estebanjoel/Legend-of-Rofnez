using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Item
{
    public class HealPickup : ItemPickup
    {
        [SerializeField] float pointToHeal;
        [SerializeField] GameObject healVFX;
        public override void UseItem(GameObject player)
        {
            player.GetComponent<Health>().Heal(pointToHeal);
            player.GetComponent<Health>().SpawnShader(healVFX);
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
