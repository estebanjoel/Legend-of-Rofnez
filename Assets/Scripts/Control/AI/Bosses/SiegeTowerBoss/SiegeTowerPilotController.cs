using System.Collections;
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
        Transform towerPosition;
        [SerializeField] Transform towerControlPosition;
        [SerializeField] float ySpeed;
        [Header("Audio Clips")]
        AudioManager audioManager;
        [SerializeField] AudioClip laughClip;
        [SerializeField] AudioClip losingBalanceClip;
        [SerializeField] AudioClip fallScreamClip;
        [SerializeField] AudioClip fallToTheFloorClip;

        
        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            anim = GetComponent<Animator>();
            myPos = transform.position;
            towerPosition = patrolPath.transform.GetChild(patrolPath.transform.childCount - 1);
            audioManager = GameObject.FindObjectOfType<AudioManager>();
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
                                audioManager.TryToPlayClip(audioManager.EnemySFXSources, losingBalanceClip);
                                isFalling = true;
                            }
                        }
                    }
                }
            }
        }

        public bool CheckIfIsOnTower()
        {
            return onTowerControl;
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
            if(transform.position.y >= towerControlPosition.position.y)
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
            return siegeTowerController.GetComponent<Health>().IsDead();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, encounterDistance);
        }

        private IEnumerator StartMoveToTowerCo()
        {
            audioManager.TryToPlayClip(audioManager.EnemySFXSources, laughClip);
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

        private void FallFromTower()
        {
            audioManager.TryToPlayClip(audioManager.EnemySFXSources, fallScreamClip);
            audioManager.TryToPlayClip(audioManager.EnemySFXSources, fallToTheFloorClip);
            transform.parent = null;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f,3f)), ForceMode.VelocityChange);
            rb.AddExplosionForce(500, transform.position, 100, 100, ForceMode.Force);
        }
    }

}