using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Item
{
    public abstract class ItemPickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                UseItem(other.gameObject);
                Destroy(this.gameObject);
            }    
        }

        public abstract void UseItem(GameObject player);
    }
}
