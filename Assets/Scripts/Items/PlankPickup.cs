using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Item
{
    public class PlankPickup : ItemPickup
    {
        public override void UseItem(GameObject player)
        {
            GameObject.FindObjectOfType<BridgeObstacle>().CollectPlank();
        }
    }

}