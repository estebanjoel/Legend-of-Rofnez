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
        SiegeTowerBossHealth siegeTowerBossHealth;
        bool canStartAction;
        BoxCollider boxCollider;

        [Header("Attack Variables")]
        [SerializeField] GameObject cannonBomb;
        [SerializeField] float cannonBombYInitPos;
        bool canAttack;
        int timesUntilStopAttack = 0;
        int timesHasAttacked = 0;
        [SerializeField] int[] attackTimes;
        int attackTimesIndex = 0;
        float attackRate;
        [SerializeField] float[] attackRates;
        int attackRatesIndex = 0;
        AttackRange attackRange;

        [Header("Shield Variables")]
        [SerializeField] GameObject shieldField;
        int percentageToSpawnCrystal = 0;
        [SerializeField] int[] crystalPercentages;
        int crystalPercentagesIndex = 0;
        bool crystalSpawned = false;

        private void Start()
        {
            wheels.SetActive(false);
            siegeTowerBossHealth = GetComponent<SiegeTowerBossHealth>();
            attackRange = GetComponent<AttackRange>();
            boxCollider = GetComponent<BoxCollider>();
            boxCollider.enabled = false;
            SetElementsWithIndexs();
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
                    EnableShield();
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
                        InvencibilityBehaviour();
                    }
               }
           }
        }

        private void AttackBehaviour()
        {
            if (timesHasAttacked == 0) StartCoroutine(BombAttackCo());
        }

        private void InvencibilityBehaviour()
        {
            if(siegeTowerBossHealth.CheckIfICanActivateShield())
            {
                EnableShield();
                crystalSpawned = false;
            }
        }

        private void SetTimesToAttack(int i)
        {
            timesUntilStopAttack = attackTimes[i];
        }

        private void SetAttackRate(int i)
        {
            attackRate = attackRates[i];
        }

        private void SetPercentageToSpawnCrystal(int i)
        {
            percentageToSpawnCrystal = crystalPercentages[i];
        }

        private bool CheckIfICanSpawnCrystal()
        {
            int percentage = Random.Range(0, 101);
            if (percentage <= percentageToSpawnCrystal)
            {
                return true;
            }
            else return false;
        }
        private void DropBomb()
        {
            Vector3 bombInitPos = new Vector3(UnityEngine.Random.Range(attackRange.GetLeftAttackLimit(), attackRange.GetRightAttackLimit()), cannonBombYInitPos, UnityEngine.Random.Range(attackRange.GetFrontAttackLimit(), attackRange.GetBackAttackLimit()));
            GameObject bomb = Instantiate(cannonBomb, bombInitPos, transform.rotation);
            if (CheckIfICanSpawnCrystal() && !crystalSpawned)
            {
                crystalSpawned = true;
                bomb.GetComponent<CannonBomb>().SetIfICanSpawnCrystal(true);
            }
        }

        private IEnumerator BombAttackCo()
        {
            while (timesHasAttacked < timesUntilStopAttack)
            {
                timesHasAttacked++;
                yield return new WaitForSeconds(attackRate);
                DropBomb();
            }
            yield return new WaitForSeconds(1f);
            timesHasAttacked = 0;
        }

        private void EnableShield()
        {
            shieldField.SetActive(true);
            siegeTowerBossHealth.SetInvencibility(true);
            boxCollider.enabled = false;
            SetElementsWithIndexs();
        }

        public void DisableShield()
        {
            shieldField.SetActive(false);
            siegeTowerBossHealth.SetInvencibility(false);
            boxCollider.enabled = true;
        }

        private void SetElementsWithIndexs()
        {
            int index = siegeTowerBossHealth.SetIndex();
            attackTimesIndex = index;
            attackRatesIndex = index;
            crystalPercentagesIndex = index;
            SetAttackRate(attackRatesIndex);
            SetPercentageToSpawnCrystal(crystalPercentagesIndex);
            SetTimesToAttack(attackTimesIndex);
        }
    }

}