using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Item
{
    public class TowerCrystalPickup : ItemPickup
    {
        public override void UseItem(GameObject player)
        {
            GameObject.FindObjectOfType<SiegeTowerController>().DisableShield();
        }
    }
}
