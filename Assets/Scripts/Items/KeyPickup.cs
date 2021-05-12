using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Item
{
    public class KeyPickup : ItemPickup
    {
        public override void UseItem(GameObject player)
        {
            Debug.Log("You have picked the key");
        }
    }
}
