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
        Animator anim;
        [SerializeField] Health target;
        float timeSinceLastAttack = 0;
        private void Start()
        {
            mover = GetComponent<Mover>();  
            anim = GetComponent<Animator>();
        }
        
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            
            //Chequeo si tengo un objetivo y si el objetivo está muerto            
            if(target == null) return;
            if(target.IsDead()) return;

            //Si estoy en el rango de ataque, ataco y sino me muevo hasta el objetivo

            if(!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                AttackBehaviour();
                mover.Cancel();
            }
        }

        //Evento en la animación de Attack. Realiza el daño al objetivo.
        void Hit()
        {
            if(target == null) return;
            target.TakeDamage(weaponDamage);
        }

        //Lo que hago al atacar
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform); //Roto hacia mi objetivo
            if(timeSinceLastAttack >= timeBetweenAttacks)
            {
                //Esto invoca el evento Hit()
                timeSinceLastAttack = 0f;
                AttackTriggers("StopAttack", "Attack");
            }
        }

        //Reinicio un trigger e inicio el otro. Esto se realizó para evitar un bug cuando se cancela un ataque con el trigger StopAttack
        private void AttackTriggers(string triggerToReset, string triggerToSet)
        {
            anim.ResetTrigger(triggerToReset);
            anim.SetTrigger(triggerToSet);
        }

        //Devuelve la distancia entre mi posición y la del objetivo y chequea que sea menor al rango del arma
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        //Inicio la acción de ataque y defino mi objetivo
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        //Chequeo si puedo atacar
        public bool CanAttack(CombatTarget combatTarget)
        {
            if(combatTarget == null) return false; //Chequeo si el objetivo es null
            Health targetToTest =  combatTarget.GetComponent<Health>();
            return !targetToTest.IsDead(); //Chequeo si el objetivo no está muerto
        }

        //Cancelo el ataque
        public void Cancel()
        {
            target = null;
            AttackTriggers("Attack", "StopAttack");
        }

    }
}
