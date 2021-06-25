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
            isLoadingScene = false;
            failCondiction = false;
            currentScene = sceneLoader.GetCurrentScene();
            player.SetStartingHealthSettings();
            player.GetComponent<MagicPoints>().SetStartingMagicPointsSettings();
            player.GetComponentInChildren<FieldOfView>().SetFogProjectorToView();
            CamaraFollower camaraFollower = GameObject.FindObjectOfType<CamaraFollower>();
            camaraFollower.SetCameraStartingSettings();
            questManager = GameObject.FindObjectOfType<QuestManager>();
            GameObject.FindObjectOfType<QuestManager>().StartingSettings();
            GetComponent<Deathcounter>().RestartCounter();
            GameObject.FindObjectOfType<ArenaObstacle>().SetEventText();
        }
    }
}
