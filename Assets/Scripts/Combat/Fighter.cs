using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.UI;

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
        GameObject shieldInstantiated;
        Mover mover;
        Animator anim;
        RuntimeAnimatorController defaultRuntimeAnimatorController;
        [SerializeField] Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        AttackTrigger weaponAttackTrigger;
        WeaponInventorMenu weaponInventoryMenu;

        private void Start()
        {
            mover = GetComponent<Mover>();  
            anim = GetComponent<Animator>();
            defaultRuntimeAnimatorController = anim.runtimeAnimatorController;
            weaponInventoryMenu = GameObject.FindObjectOfType<WeaponInventorMenu>();
            EquipWeapon(defaultWeapon);
            if(gameObject.tag == "Player")
            {
                WeaponInventory playerWeaponInventory = GetComponent<WeaponInventory>();
                if(defaultWeapon.HasProjectile())
                {
                    playerWeaponInventory.SetRangedWeapon(defaultWeapon);
                    playerWeaponInventory.SetActiveWeapon(playerWeaponInventory.GetRangedWeapon());
                } 
                else
                {
                    playerWeaponInventory.SetMeleeWeapon(defaultWeapon);
                    playerWeaponInventory.SetActiveWeapon(playerWeaponInventory.GetMeleeWeapon());
                } 
                
            }
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
            anim.runtimeAnimatorController = defaultRuntimeAnimatorController;
            weapon.Spawn(rightHandTransform, leftHandTransform, anim);
            if(currentWeapon != null && !currentWeapon.HasProjectile())
            {
                if (currentWeapon.CheckIsRightHanded()) weaponAttackTrigger = rightHandTransform.GetChild(rightHandTransform.childCount - 1).GetComponent<AttackTrigger>();
                else weaponAttackTrigger = leftHandTransform.GetChild(leftHandTransform.childCount - 1).GetComponent<AttackTrigger>();

                weaponAttackTrigger.SetTriggerDamage(currentWeapon.GetWeaponDamage());
                weaponAttackTrigger.DeactivateWeaponCollider();
            }

            if(gameObject.tag == "Player") ChangeInventoryUI();
        }

        public void ChangeInventoryUI()
        {
            int currentWeaponSprite = -1;
            Debug.Log(currentWeapon.name);
            if(currentWeapon.HasProjectile())
            {
                weaponInventoryMenu.SetRangedWeaponSprite(currentWeapon.GetWeaponSprite());
                weaponInventoryMenu.SetInventoryRangedSprite();
                weaponInventoryMenu.SetRangedInventoryText(currentWeapon.name);
                currentWeaponSprite = 1;
                weaponInventoryMenu.SetAmmoText(GetComponent<RangedWeaponAmmoInventory>().GetAmmo().ToString());
            } 
            else
            {
                weaponInventoryMenu.SetMeleeWeaponSprite(currentWeapon.GetWeaponSprite());
                weaponInventoryMenu.SetInventoryMeleeSprite();
                weaponInventoryMenu.SetMeleeInventoryText(currentWeapon.name);
                currentWeaponSprite = 0;
            }
            weaponInventoryMenu.SetCurrentWeaponActive(currentWeaponSprite);
        }

        public Weapon GetCurrentWeapon()
        {
            return currentWeapon;
        }

        public void EquipShield(Shield shield)
        {
            currentShield = shield;
            if (shield == null) return;
            shield.SpawnShield(leftHandTransform);
            shieldInstantiated = leftHandTransform.GetChild(leftHandTransform.childCount - 1).gameObject;
        }

        public Shield GetCurrentShield()
        {
            return currentShield;
        }

        public void ShowShield()
        {
            shieldInstantiated.SetActive(true);
        }

        public void HideShield()
        {
            shieldInstantiated.SetActive(false);
        }

        //Evento en la animación de Attack. Realiza el daño al objetivo.
        void Hit()
        {
            if(target == null) return;

            if (currentWeapon.HasProjectile()) //Chequeo si es un proyectil
            {
                if(gameObject.tag == "Player")
                {
                    int currentAmmo = GetComponent<RangedWeaponAmmoInventory>().GetAmmo() - 1;
                    GetComponent<RangedWeaponAmmoInventory>().SetAmmo(currentAmmo);
                    weaponInventoryMenu.SetAmmoText(currentAmmo.ToString());
                    currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
                }
                else
                {
                    currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target); 
                }
            } 
            else
            {
                weaponAttackTrigger.ActivateWeaponCollider();
            } 
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
                if(gameObject.tag == "Player")
                {
                    if(currentWeapon == GetComponent<WeaponInventory>().GetRangedWeapon() && GetComponent<RangedWeaponAmmoInventory>().GetAmmo() > 0) AttackTriggers("StopAttack", "Attack");
                    else if(currentWeapon == GetComponent<WeaponInventory>().GetMeleeWeapon()) AttackTriggers("StopAttack", "Attack");
                }
                else
                {
                    AttackTriggers("StopAttack", "Attack");
                }
                
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
