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
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float timeBetweenAttacks = 1f;
        Mover mover;
        [SerializeField] Transform target;
        float timeSinceLastAttack = 0;
        private void Start()
        {
            mover = GetComponent<Mover>();  
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            
            if(target == null) return;
            
            if(!GetIsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                AttackBehaviour();
                mover.Cancel();
            }
        }

        //Animation Event
        void Hit()
        {
            target.GetComponent<Health>().TakeDamage(weaponDamage);
        }
        private void AttackBehaviour()
        {
            if(timeSinceLastAttack >= timeBetweenAttacks)
            {
                //This will trigger the hit event
                timeSinceLastAttack = 0f;
                GetComponent<Animator>().SetTrigger("Attack");
            }
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
