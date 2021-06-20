using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public abstract class AIController: MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        ActionScheduler actionScheduler;
        Fighter fighter;
        Health health;
        GameObject player;
        Mover mover;
        public void ParentStartingSettings()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
        }

        // Getters Principales
        public ActionScheduler GetActionScheduler()
        {
            return actionScheduler;
        }

        public Fighter GetFighter()
        {
            return fighter;
        }

        public Health GetHealth()
        {
            return health;
        }

        public Mover GetMover()
        {
            return mover;
        }

        public GameObject GetPlayer()
        {
            return player;
        }

        public float GetChaseDistance()
        {
            return chaseDistance;
        }

        //Update del Padre
        private void Update()
        {
            //Chequeo si está muerto
            if (health.IsDead()) return;
            UpdateBehaviour();
            UpdateTimers();
        }

        public abstract void UpdateTimers();

        // private void UpdateTimers()
        // {
        //     timeSinceLastSawPlayer += Time.deltaTime;
        //     timeSinceMovedToNextWaypoint += Time.deltaTime;
        // }

        public abstract void AttackBehaviour();

        public abstract void UpdateBehaviour();

         //Devuelve un booleano al calcular la distancia del player con el enemigo y si dicha distancia es menor a la distancia para perseguir al player.
        public bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(GetPlayer().transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}