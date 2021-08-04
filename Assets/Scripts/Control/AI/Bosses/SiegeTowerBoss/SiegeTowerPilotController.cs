﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class SiegeTowerPilotController : MonoBehaviour
    {
        [SerializeField] float encounterDistance = 3.0f;
        [SerializeField] SiegeTowerController siegeTowerController;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        NavMeshAgent navMeshAgent;
        Animator anim;
        GameObject player;
        Mover mover;
        Vector3 myPos;
        bool canStartFirstCoroutine = true;
        bool canMoveToTower;
        bool onTower;
        bool onTowerControl;
        bool isFalling;
        int currentWaypointIndex = 0;
        [SerializeField] Transform towerPosition;
        [SerializeField] Transform towerControlPosition;
        [SerializeField] float ySpeed;

        
        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            anim = GetComponent<Animator>();
            myPos = transform.position;
            towerPosition = patrolPath.transform.GetChild(patrolPath.transform.childCount - 1);
        }

        // Update is called once per frame
        void Update()
        {
            if(!isFalling)
            {
                if (!canMoveToTower)
                {
                    if (CheckEncounterDistance())
                    {
                        if (canStartFirstCoroutine) StartCoroutine(StartMoveToTowerCo());
                    }
                }
                else
                {
                    if (!onTower)
                    {
                        MoveToTower();
                    }
                    else
                    {
                        if (!onTowerControl)
                        {
                            MoveToTowerControl();
                        }
                        else
                        {
                            if (CheckIfTowerIsDestroyed())
                            {
                                anim.SetTrigger("deadTrigger");
                                isFalling = true;
                            }
                        }
                    }
                }
            }
        }

        private bool CheckEncounterDistance()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < encounterDistance;
        }

        private void MoveToTower()
        {
            Vector3 nextPos = myPos;
            if (GetCurrentWaypoint() == towerPosition.position)
            {
                if (AtWaypoint())
                {
                    StartCoroutine(DisableMovementCo());
                }
            }
            else
            {
                if(AtWaypoint())
                {
                    CycleWaypoint();
                }

                nextPos = GetCurrentWaypoint();
                mover.StartMoveAction(nextPos);
            }
        }


        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
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

        private void MoveToTowerControl()
        {
            if(transform.position.y >= towerControlPosition.localPosition.y)
            {
                //transform.LookAt(towerControlPosition);
                onTowerControl = true;
                transform.parent = siegeTowerController.transform;
            }
            else
            {
                transform.position += transform.up * ySpeed * Time.deltaTime;
            }
        }

        private bool CheckIfTowerIsDestroyed()
        {
            if (Input.GetKeyDown(KeyCode.W)) return true;
            else return false;
            //return siegeTowerController.GetComponent<Health>().IsDead();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, encounterDistance);
        }

        private IEnumerator StartMoveToTowerCo()
        {
            canStartFirstCoroutine = false;
            transform.LookAt(player.transform.position);
            anim.SetTrigger("encounterTrigger");
            yield return new WaitForSeconds(5f);
            canMoveToTower = true;
        }

        private IEnumerator DisableMovementCo()
        {
            yield return new WaitForSeconds(0.5f);
            mover.enabled = false;
            navMeshAgent.enabled = false;
            onTower = true;
            anim.SetTrigger("onTower");
        }
    }

}