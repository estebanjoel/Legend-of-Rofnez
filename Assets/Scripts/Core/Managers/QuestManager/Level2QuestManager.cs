using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Level2QuestManager : QuestManager
    {
        CollectableIdols collectableIdols;
        [SerializeField] GameObject angelStatue;
        [SerializeField] GameObject angelStatueDestroyable;
        bool destroyableHasBeenActivated;
        [SerializeField] GameObject key;
        bool keyHasAppeared;
        [SerializeField] WizardBossHealth boss;
        GameObject bossHealthBar;
        // Start is called before the first frame update
        public override void LevelStartingSettings()
        {
            collectableIdols = GetComponent<CollectableIdols>();
            angelStatueDestroyable.SetActive(false);
            key.SetActive(false);
            boss = GameObject.FindObjectOfType<WizardBossHealth>();
            bossHealthBar = GameObject.Find("BossHealthBar");
            bossHealthBar.SetActive(false);
            collectableIdols.SetEventText();
        }
        public override void QuestChecker()
        {
            if(collectableIdols.CheckIfAllIdolsWereCollected()) // Chequea si todos los ídolos fueron recolectados
            {
                if(!destroyableHasBeenActivated) // Chequea si la estatua destruible ha aparecido, sino, destruye la estatua y muestra la estatua destruible
                {
                    if(angelStatue != null) Destroy(angelStatue);
                    angelStatueDestroyable.SetActive(true);
                    destroyableHasBeenActivated = true;
                    collectableIdols.AllIdolsCollectedText();
                }
                else
                {
                    if(angelStatueDestroyable == null) // Chequea si la estatua distruible ya no está
                    {
                        if(!keyHasAppeared)
                        {
                            key.SetActive(true);
                            keyHasAppeared = true;
                            bossHealthBar.SetActive(true);
                        }
                        else
                        {
                            if(boss.IsDead()) CompleteQuests(); //Si el boss muere, la quest se ha completado
                        } 
                    }
                }
            }
        }
    }
}
