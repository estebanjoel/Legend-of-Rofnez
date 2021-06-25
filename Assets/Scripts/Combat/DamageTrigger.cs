using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class DamageTrigger : MonoBehaviour
    {
        [SerializeField] float damageToDeal;

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                other.GetComponent<Health>().TakeDamage(damageToDeal);
            }    
        }
        
    }

}