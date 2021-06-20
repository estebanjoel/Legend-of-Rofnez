using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Combat;

namespace RPG.Control
{
    public class StoneBossAIController : AIController
    {
        [SerializeField] float fireballAttackTime = 5f;
        float timeSinceThrewFireball = Mathf.Infinity;
        bool castedFireball;
        [SerializeField] float attackBehaviourTime = 1f;
        float timeUntilICanAttack = 0;
        MagicPoints magicPoints;
        Special special;
        void Start()
        {
            ParentStartingSettings();
            magicPoints = GetComponent<MagicPoints>();
            special = GetComponent<Special>();
        }

        public override void UpdateBehaviour()
        {
            if(timeSinceThrewFireball >= fireballAttackTime) castedFireball = false;
            
            if(!castedFireball)
            {
                if (InAttackRangeOfPlayer()) CastFireball(); 
            }
            else
            {
                if(timeUntilICanAttack >= attackBehaviourTime)
                {
                    if (InAttackRangeOfPlayer() && GetFighter().CanAttack(GetPlayer())) AttackBehaviour();
                }
            }
        }

        public override void UpdateTimers()
        {
            timeSinceThrewFireball += Time.deltaTime;
            timeUntilICanAttack += Time.deltaTime;
        }

        public override void AttackBehaviour()
        {
            GetFighter().Attack(GetPlayer());
        }

        //Llama Unity para mostrar los gizmos que se dibujen en esta sección
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, GetChaseDistance());
        }

        // Funciones Propias del Stone Boss
        public void CastFireball()
        {
            castedFireball = true;
            timeSinceThrewFireball = 0;
            timeUntilICanAttack = 0;
            special.SpecialAttack();
        }
    }
}
