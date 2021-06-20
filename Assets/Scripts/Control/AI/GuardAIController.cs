using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class GuardAIController : AIController
    {
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        Vector3 guardPosition;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 5f;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;
        float timeSinceMovedToNextWaypoint = Mathf.Infinity;
        void Start()
        {
            ParentStartingSettings();
            guardPosition = transform.position;
        }
        
        public override void UpdateBehaviour()
        {
            if (InAttackRangeOfPlayer() && GetFighter().CanAttack(GetPlayer())) AttackBehaviour(); //Chequeo si el player está en mi rango de ataque y si puedo atacar.
            else if (timeSinceLastSawPlayer <= suspicionTime) SuspicionBehaviour(); //Chequeo si aún está en guardia o vuelvo a patrullar
            else PatrolBehaviour(); //Patrulla
        }
        public override void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            GetFighter().Attack(GetPlayer());
        }

        public override void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceMovedToNextWaypoint += Time.deltaTime;
        }

        private void SuspicionBehaviour()
        {
            GetActionScheduler().CancelCurrentAction();
        }


        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if(patrolPath != null)
            {
                if(AtWaypoint())
                {
                    timeSinceMovedToNextWaypoint = 0f;
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }
            if(timeSinceMovedToNextWaypoint > waypointDwellTime)
            {
                GetMover().StartMoveAction(nextPosition);
            }
            
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        //Llama Unity para mostrar los gizmos que se dibujen en esta sección
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, GetChaseDistance());
        }
    }
}
