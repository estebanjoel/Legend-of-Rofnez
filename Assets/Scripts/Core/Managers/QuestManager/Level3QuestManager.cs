using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Level3QuestManager : QuestManager
    {
        [SerializeField] SiegeTowerBossHealth boss;
        GameObject bossHealthBar;
        BridgeObstacle bridgeObstacle;
        
        public override void LevelStartingSettings()
        {
            boss = GameObject.FindObjectOfType<SiegeTowerBossHealth>();
            bossHealthBar = GameObject.Find("BossHealthBar");
            bossHealthBar.SetActive(false);
            bridgeObstacle = GameObject.FindObjectOfType<BridgeObstacle>();
            bridgeObstacle.StartingSettings();
        }
        public override void QuestChecker()
        {
            if(bridgeObstacle.CheckIfBridgeIsBuild())
            {
                if(!bossHealthBar.activeInHierarchy) bossHealthBar.SetActive(true);
                if(boss.IsDead()) CompleteQuests();
            }
        }

    }

}