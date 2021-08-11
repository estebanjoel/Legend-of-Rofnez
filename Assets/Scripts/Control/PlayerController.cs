using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.UI;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Fighter fighter;
        Special special;
        Health health;
        WeaponInventory weaponInventory;
        PlayerCursor playerCursor;
        private void Start()
        {
            fighter = GetComponent<Fighter>();
            special = GetComponent<Special>();
            health = GetComponent<Health>();
            weaponInventory = GetComponent<WeaponInventory>();
            playerCursor = GetComponent<PlayerCursor>();
        }
        void Update()
        {
            if(health.IsDead()) return;
            if(ActivateSpecialAttack()) return;
            if(InteractWithCombat()) return;
            if(Input.GetKeyDown(KeyCode.Q))
            {
                ChangePlayerActiveWeapon();
            }
            if(InteractWithMovement()) return;
        }

        private bool ActivateSpecialAttack()
        {
            if(Input.GetMouseButtonDown(1) && special.getCurrentMagic() != null)
            {
                special.SpecialAttack();
                return true;
            }
            else return false;
        }
        
        // Busco con raycast si encuentro un objetivo para pelear, chequeo si puedo atacarlo y si hago click, lo ataco
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
                playerCursor.SetFightCursor();
                if(target == null) continue;
                if(!fighter.CanAttack(target.gameObject)) continue;
                if(Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target.gameObject);
                } 
                return true;
            }
            playerCursor.SetDefaultCursor();
            return false;
        }

        //Chequeo con Raycast algún punto en el mundo en donde pueda hacer moverme y si hago click y tengo lugar, me muevo
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit && !CheckRaycastTags(hit))
            {
                playerCursor.SetMoveCursor();
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            playerCursor.SetDefaultCursor();
            return false;
        }

        //Devuelve el punto donde esté apuntando con el mouse en la posición del mundo
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void ChangePlayerActiveWeapon()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
            if (weaponInventory.GetActiveWeapon() == weaponInventory.GetMeleeWeapon() && weaponInventory.GetRangedWeapon() != null)
            {
                weaponInventory.SetActiveWeapon(weaponInventory.GetRangedWeapon());
            }
            else if (weaponInventory.GetActiveWeapon() == weaponInventory.GetRangedWeapon() && weaponInventory.GetMeleeWeapon() != null)
            {
                weaponInventory.SetActiveWeapon(weaponInventory.GetMeleeWeapon());
            }
            fighter.EquipWeapon(weaponInventory.GetActiveWeapon());

            if(fighter.GetCurrentShield() != null)
            {
                if (fighter.GetCurrentWeapon() == weaponInventory.GetMeleeWeapon()) fighter.ShowShield();
                else fighter.HideShield();
            }
        }

        private bool CheckRaycastTags(RaycastHit hitPoint)
        {
            switch(hitPoint.collider.gameObject.tag)
            {
                case "Prop":
                    return true;
                default:
                    return false;
            }
        }
    }
}