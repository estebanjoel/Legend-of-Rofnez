using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        PlayerHealth player;
        BossHealth boss;
        RPGSceneManager rpgSceneManager;
        [SerializeField] string loseScene;
        [SerializeField] string winScene;

        bool victoryCondition;
        bool failCondiction;
        bool isLoadingScene;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindObjectOfType<PlayerHealth>();
            boss = GameObject.FindObjectOfType<BossHealth>();
            victoryCondition = false;
            failCondiction = false;
            rpgSceneManager = GetComponent<RPGSceneManager>();
        }

        // Update is called once per frame
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

        private void LoadNextScene(string newScene)
        {
            StartCoroutine(NextSceneCo(newScene));
        }

        private IEnumerator NextSceneCo(string newScene)
        {
            rpgSceneManager.SetSceneToLoad(newScene);
            yield return new WaitForSeconds(1f);
            rpgSceneManager.LoadScene();
            isLoadingScene = false;
        }
    }
}
