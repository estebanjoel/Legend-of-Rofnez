using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class RewardDrop : MonoBehaviour
    {
        [SerializeField] GameObject rewardToDrop;
        [SerializeField] int dropRate;
        int percentageToDrop;

        public void CheckIfCanDropReward()
        {
            percentageToDrop = Random.Range(0, 100);
            if(percentageToDrop <= dropRate)
            {
                DropReward();
            }
        }

        public void DropReward()
        {
            Instantiate(rewardToDrop, transform.position, transform.rotation);
        }
    }
}

