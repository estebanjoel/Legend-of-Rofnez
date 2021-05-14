using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Control;

namespace RPG.Item
{
       public class MagicPickup : ItemPickup
    {
        [SerializeField] Magic magic;
        [SerializeField] int indexToAdd;
        public override void UseItem(GameObject player)
        {
            player.GetComponent<MagicCollection>().AddMagic(magic, indexToAdd);
            player.GetComponent<Special>().setCurrentMagic(magic);
        }
    }
}
