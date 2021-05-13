using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
    public class RPGSceneManager : MonoBehaviour
    {
        string sceneToLoad;
        string currentScene;

        public void SetSceneToLoad(string newScene)
        {
            sceneToLoad = newScene;
        }

        public string GetSceneToLoad()
        {
            return sceneToLoad;
        }
        public void SetCurrentScene(string newScene)
        {
            currentScene = newScene;
        }

        public string GetCurrentScene()
        {
            return currentScene;
        }

        public void LoadScene()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
