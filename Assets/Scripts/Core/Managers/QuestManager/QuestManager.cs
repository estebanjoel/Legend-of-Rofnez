using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.SceneManagement;

namespace RPG.Core
{
    public abstract class QuestManager : MonoBehaviour
    {
        [SerializeField] GameObject nextLevelPortal;
        bool allQuestsAreCompleted = false;
        public void StartingSettings()
        {
            nextLevelPortal.SetActive(false);
            LevelStartingSettings();
        }

        public abstract void LevelStartingSettings();

        public bool CheckIfAllQuestsAreCompleted()
        {
            return allQuestsAreCompleted;
        }

        public void CompleteQuests()
        {
            allQuestsAreCompleted = true;
        }

        public abstract void QuestChecker();

        public void ShowNextLevelPortal()
        {
            if(!nextLevelPortal.activeInHierarchy) nextLevelPortal.SetActive(true);
        }

        public Portal GetPortal()
        {
            return nextLevelPortal.GetComponent<Portal>();
        }
    }
}
