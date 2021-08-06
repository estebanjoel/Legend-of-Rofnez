using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class SiegeTowerController : MonoBehaviour
    {
        [SerializeField] SiegeTowerPilotController siegeTowerPilotController;
        [SerializeField] GameObject invisibleWall;
        [SerializeField] GameObject wheels;
        [SerializeField] GameObject cannonBomb;
        SiegeTowerBossHealth siegeTowerBossHealth;
        bool canStartAction;
        BoxCollider boxCollider;

        [Header("Attack Variables")]
        bool canAttack;
        [SerializeField] float attackRate;
        [SerializeField] int timesUntilStopAttack;
        [SerializeField] int[] attackTimes;

        private void Start()
        {
            wheels.SetActive(false);
            siegeTowerBossHealth = GetComponent<SiegeTowerBossHealth>();
            boxCollider = GetComponent<BoxCollider>();
            boxCollider.enabled = false;
        }

        void Update()
        {
           if(!canStartAction)
           {
                if(siegeTowerPilotController.CheckIfIsOnTower())
                {
                    invisibleWall.SetActive(false);
                    wheels.SetActive(true);
                    canStartAction = true;
                    boxCollider.enabled = true;
                }
           }
           else
           {
               if(!siegeTowerBossHealth.IsDead())
               {
                   if(siegeTowerBossHealth.CheckInvencibility())
                   {
                       AttackBehaviour();
                   }
                   else
                   {
                       
                   }
               }
           }
        }

        private void AttackBehaviour()
        {

        }

        private void InvencibilityBehaviour()
        {

        }
    }

}