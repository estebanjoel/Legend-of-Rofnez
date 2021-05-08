using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        Mover mover;
        [SerializeField] Transform target;
        private void Start()
        {
            mover = GetComponent<Mover>();  
        }
        private void Update()
        {
            if(target == null) return;
            
            // if(!GetIsInRange())
            // {
            //     mover.MoveTo(target.position);
            // }
            // else
            // {
            //     mover.Cancel();
            // }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}
