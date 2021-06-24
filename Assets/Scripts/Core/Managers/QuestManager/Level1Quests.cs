using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class Level1Quests : QuestManager
    {
        [SerializeField] GameObject key;
        bool keyHasAppeared = false;
        [SerializeField] int deathsNeeded = 7;
        Deathcounter deathcounter;
        BossHealth boss;
        EventText eventText;
        GameObject bossHealthBar;

        public override void LevelStartingSettings()
        {
            deathcounter = GameObject.FindObjectOfType<Deathcounter>();
            boss = GameObject.FindObjectOfType<BossHealth>();
            boss.SetHealthBar();
            key.SetActive(false);
            eventText = GameObject.FindObjectOfType<EventText>();
            bossHealthBar = GameObject.Find("BossHealthBar");
            bossHealthBar.SetActive(false);
        }
        
        public override void QuestChecker()
        {
            if(!keyHasAppeared)
            {
                if(deathcounter.GetCounter() >= deathsNeeded)
                {
                    eventText.SetEventText("You can get the Key hidden in the forest!");
                    key.SetActive(true);
                    keyHasAppeared = true;
                    bossHealthBar.SetActive(true);
                }   
            }
            else
            {
                if(boss.IsDead())
                {
                    CompleteQuests();
                }
            }
        }
    }
}
