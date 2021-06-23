using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Item
{
    public class IdolPickup : ItemPickup
    {
       [SerializeField] int idolID;
       public override void UseItem(GameObject player)
       {
           GameObject.FindObjectOfType<CollectableIdols>().CollectIdol(idolID);
       }
    }

}