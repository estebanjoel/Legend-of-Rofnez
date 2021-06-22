using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Item
{
    public class ManaPickup : ItemPickup
    {
        [SerializeField] float magicPointsToHeal;
        [SerializeField] GameObject mpVFX;

        public override void UseItem(GameObject player)
        {
            player.GetComponent<MagicPoints>().RestoreMagicPoints(magicPointsToHeal);
            player.GetComponent<Health>().SpawnShader(mpVFX);
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }

}