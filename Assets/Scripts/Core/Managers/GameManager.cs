using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        PlayerHealth player;
        BossHealth boss;
        RPGSceneManager rpgSceneManager;
        Deathcounter deathcounter;
        EventText eventText;
        [SerializeField] string loseScene;
        [SerializeField] string winScene;
        [SerializeField] GameObject key;
        [SerializeField] int deathsNeeded;
        bool victoryCondition;
        bool failCondiction;
        bool isLoadingScene;
        bool keyHasAppeared = false;
        [SerializeField] GameObject pauseMenuUI;
        [SerializeField] static bool GameIsPaused = false;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindObjectOfType<PlayerHealth>();
            boss = GameObject.FindObjectOfType<BossHealth>();
            eventText = GameObject.FindObjectOfType<EventText>();
            victoryCondition = false;
            failCondiction = false;
            rpgSceneManager = GetComponent<RPGSceneManager>();
            deathcounter = GetComponent<Deathcounter>();
            key.SetActive(false);
        }

        void Update()
        {
            if(!keyHasAppeared)
            {
                if(deathcounter.GetCounter() >= deathsNeeded)
                {
                    eventText.SetEventText("You can get the Key hidden in the forest!");
                    key.SetActive(true);
                    keyHasAppeared = true;
                }   
            }
        }
        
        void LateUpdate()
        {
            if(!isLoadingScene)
            {
                if(victoryCondition)
                {
                    LoadNextScene(winScene);
                    isLoadingScene = true;
                    return;
                } 

                if(failCondiction)
                {
                    LoadNextScene(loseScene);
                    isLoadingScene = true;
                    return;
                }
                if(player.IsDead())
                {
                    victoryCondition = false;
                    failCondiction = true;
                } 

                if(boss.IsDead())
                {
                    victoryCondition = true;
                    failCondiction = false;
                }
            }

        }
        void MenuContinue() 
        {
            pauseMenuUI.SetActive(false); ;
            Time.timeScale = 1;
            GameIsPaused = false;
        }
        void MenuPause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        private void LoadNextScene(string newScene)
        {
            StartCoroutine(NextSceneCo(newScene));
        }

        private IEnumerator NextSceneCo(string newScene)
        {
            rpgSceneManager.SetSceneToLoad(newScene);
            yield return new WaitForSeconds(2.5f);
            rpgSceneManager.LoadScene();
            isLoadingScene = false;
        }
    }
}
