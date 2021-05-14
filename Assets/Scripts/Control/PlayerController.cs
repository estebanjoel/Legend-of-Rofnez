using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Fighter fighter;
        Special special;
        Health health;
        private void Start()
        {
            fighter = GetComponent<Fighter>();
            special = GetComponent<Special>();
            health = GetComponent<Health>();
            
        }
        void Update()
        {
            if(health.IsDead()) return;
            if(ActivateSpecialAttack()) return;
            if(InteractWithCombat()) return;
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
                if(target == null) continue;
                if(!fighter.CanAttack(target.gameObject)) continue;
                if(Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target.gameObject);
                } 
                return true;
            }
            return false;
        }

        //Chequeo con Raycast algún punto en el mundo en donde pueda hacer moverme y si hago click y tengo lugar, me muevo
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        //Devuelve el punto donde esté apuntando con el mouse en la posición del mundo
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}