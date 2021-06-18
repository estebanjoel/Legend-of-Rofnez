using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.SceneManagement;

namespace RPG.UI
{    
    public class PauseMenu : MonoBehaviour
    {
        PauseManager pauseManager;
        SceneLoader sceneLoader;
        MenuController menuController;

        private void Start()
        {
            pauseManager = GameObject.FindObjectOfType<PauseManager>();
            sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
            menuController = GameObject.FindObjectOfType<MenuController>();
        }
        public void ResumeButton()
        {
            pauseManager.SetPause(false);
            menuController.HideUIObject(menuController.GetPauseMenu());
            menuController.ShowUIObject(menuController.GetFader());
        }

        public void ReturnToMainMenuButton(int sceneToLoad)
        {
            sceneLoader.SetSceneToLoad(sceneToLoad);
            pauseManager.SetPause(false);
            StartCoroutine(ReturnToMainMenu());
        }

        private IEnumerator ReturnToMainMenu()
        {
            ResumeButton();
            yield return sceneLoader.TransitionBeginCo();
            yield return sceneLoader.TransitionEndCo();
        }

        public void QuitGameButton()
        {
            pauseManager.SetPause(false);
            StartCoroutine(QuitCo());
        }
        
        private IEnumerator QuitCo()
        {
            ResumeButton();
            UIFade fader = GameObject.FindObjectOfType<UIFade>();
            yield return fader.FadeOut(2f);
            Application.Quit();
        }
    }
}
