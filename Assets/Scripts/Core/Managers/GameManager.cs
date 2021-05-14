﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        PlayerHealth player;
        BossHealth boss;
        RPGSceneManager rpgSceneManager;
        Deathcounter deathcounter;
        [SerializeField] string loseScene;
        [SerializeField] string winScene;
        [SerializeField] GameObject key;
        [SerializeField] int deathsNeeded;
        bool victoryCondition;
        bool failCondiction;
        bool isLoadingScene;
        bool keyHasAppeared = false;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindObjectOfType<PlayerHealth>();
            boss = GameObject.FindObjectOfType<BossHealth>();
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
