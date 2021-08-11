using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;
using RPG.SceneManagement;
using RPG.Obstacle;

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        PlayerHealth player;
        QuestManager questManager;
        PauseManager pauseManager;
        AudioManager audioManager;
        LevelMusic levelMusic;
        [SerializeField] int loseScene;
        SceneLoader sceneLoader;
        int currentScene;
        bool failCondiction;
        bool isLoadingScene;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindObjectOfType<PlayerHealth>();
            pauseManager = GetComponent<PauseManager>();
            sceneLoader = GetComponent<SceneLoader>();
            audioManager = GameObject.FindObjectOfType<AudioManager>();
            SetLevelSettings();
        }

        void Update()
        {
            if(Input.GetButtonDown("Cancel"))
            {
                if(pauseManager.GetPauseState()) pauseManager.SetPause(false);
                else pauseManager.SetPause(true);
            }
        }
        
        void LateUpdate()
        {
            if(!isLoadingScene)
            {
                if(!failCondiction)
                {
                    if(!player.IsDead()) // Si el personaje no está muerto, chequeo las quests del nivel para saber si puedo acceder al siguiente nivel
                    {
                        if(questManager.CheckIfAllQuestsAreCompleted())
                        {
                            questManager.ShowNextLevelPortal();
                            if(questManager.GetPortal().IsPlayerOnPortal()) isLoadingScene = true;
                        }  //Si las quests están completas, se mostrará el portal para acceder al siguiente nivel
                        else questManager.QuestChecker();
                    }
                    else // En caso de que el personaje esté muerto, Game Over
                    {
                        failCondiction = true;
                    }
                }

                else // Corrutina que carga la pantalla de Game Over
                {
                    isLoadingScene = true;
                    StartCoroutine(LoseSceneCo());
                    return;
                }
            }
            else
            {
                if(currentScene != sceneLoader.GetCurrentScene())
                {
                    if(sceneLoader.GetLevelSetChecker()) SetLevelSettings();
                }
            }

        }

        public IEnumerator LoseSceneCo()
        {
            sceneLoader.SetSceneToLoad(loseScene);
            yield return sceneLoader.TransitionBeginCo();
            yield return sceneLoader.TransitionEndCo();
        }

        void SetLevelSettings()
        {
            failCondiction = false;
            isLoadingScene = false;
            currentScene = sceneLoader.GetCurrentScene();
            player.SetStartingHealthSettings();
            player.GetComponent<MagicPoints>().SetStartingMagicPointsSettings();
            CamaraFollower camaraFollower = GameObject.FindObjectOfType<CamaraFollower>();
            questManager = GameObject.FindObjectOfType<QuestManager>();
            GameObject.FindObjectOfType<QuestManager>().StartingSettings();
            GetComponent<Deathcounter>().RestartCounter();
            GameObject.FindObjectOfType<ArenaObstacle>().SetEventText();
            PlayLevelMusic();
        }

        private void PlayLevelMusic()
        {
            levelMusic = GameObject.FindObjectOfType<LevelMusic>();
            audioManager.PlayClip(audioManager.BGM, levelMusic.GetBGMClip());
            audioManager.PlayClip(audioManager.Ambience, levelMusic.GetAmbienceClip());
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
