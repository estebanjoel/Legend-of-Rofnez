using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] Weapon currentWeapon = null;
        Mover mover;
        Animator anim;
        [SerializeField] Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        private void Start()
        {
            mover = GetComponent<Mover>();  
            anim = GetComponent<Animator>();
            EquipWeapon(defaultWeapon);
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

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            if(weapon == null) return;
            weapon.Spawn(rightHandTransform, leftHandTransform, anim);

        }

        //Evento en la animación de Attack. Realiza el daño al objetivo.
        void Hit()
        {
            if(target == null) return;

            if(currentWeapon.HasProjectile()) currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            else target.TakeDamage(currentWeapon.GetWeaponDamage());
        }

        void Shoot()
        {
            Hit();
        }

        //Lo que hago al atacar
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform); //Roto hacia mi objetivo
            if(timeSinceLastAttack >= currentWeapon.GetTimeBetweenAttacks())
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
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
        }

        //Inicio la acción de ataque y defino mi objetivo
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        //Chequeo si puedo atacar
        public bool CanAttack(GameObject combatTarget)
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
