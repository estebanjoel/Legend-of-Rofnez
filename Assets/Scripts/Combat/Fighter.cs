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
        [SerializeField] Shield defaultShield = null;
        [SerializeField] Shield currentShield = null;
        Mover mover;
        Animator anim;
        [SerializeField] Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        AttackTrigger weaponAttackTrigger;

        private void Start()
        {
            mover = GetComponent<Mover>();  
            anim = GetComponent<Animator>();
            EquipWeapon(defaultWeapon);
            EquipShield(defaultShield);
        }
        
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            //Chequeo si tengo un objetivo y si el objetivo está muerto            
            if (target == null) return;
            if(target.IsDead()) return;

            //Si estoy en el rango de ataque, ataco y sino me muevo hasta el objetivo
            if(GetComponent<ActionScheduler>().GetCurrentAction() != (IAction)GetComponent<Special>())
            {
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
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            if(weapon == null) return;
            weapon.Spawn(rightHandTransform, leftHandTransform, anim);
            if(currentWeapon != null && !currentWeapon.HasProjectile())
            {
                if (currentWeapon.CheckIsRightHanded()) weaponAttackTrigger = rightHandTransform.GetChild(rightHandTransform.childCount - 1).GetComponent<AttackTrigger>();
                else weaponAttackTrigger = leftHandTransform.GetChild(leftHandTransform.childCount - 1).GetComponent<AttackTrigger>();

                weaponAttackTrigger.SetTriggerDamage(currentWeapon.GetWeaponDamage());
                weaponAttackTrigger.DeactivateWeaponCollider();
            }

        }

        public void EquipShield(Shield shield)
        {
            currentShield = shield;
            if (shield == null) return;
            shield.SpawnShield(leftHandTransform);
        }

        public Shield GetCurrentShield()
        {
            return currentShield;
        }

        //Evento en la animación de Attack. Realiza el daño al objetivo.
        void Hit()
        {
            if(target == null) return;

            if (currentWeapon.HasProjectile()) currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target); //Chequeo si es un proyectil
            else weaponAttackTrigger.ActivateWeaponCollider();
            //else if(!target.CheckInvencibility()) target.TakeDamage(currentWeapon.GetWeaponDamage()); //Chequeo si no es invencible, si es falso, ejecuto el daño            
        }

        void StopHit()
        {
            weaponAttackTrigger.DeactivateWeaponCollider();
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
            if (weaponAttackTrigger != null) weaponAttackTrigger.DeactivateWeaponCollider();
        }

    }
}
