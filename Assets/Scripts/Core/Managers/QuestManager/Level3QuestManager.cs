using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Level3QuestManager : QuestManager
    {
        [SerializeField] SiegeTowerBossHealth boss;
        GameObject bossHealthBar;
        
        public override void LevelStartingSettings()
        {
            boss = GameObject.FindObjectOfType<SiegeTowerBossHealth>();
            bossHealthBar = GameObject.Find("BossHealthBar");
            bossHealthBar.SetActive(false);
        }
        public override void QuestChecker()
        {
            if(boss.IsDead()) CompleteQuests();
        }

    }

}