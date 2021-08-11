using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using RPG.UI;

namespace RPG.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;
        [SerializeField] PersistantObjectDestroyer persistantObjectDestroyer;
        [SerializeField] int lastPlayableScene;
        int previousScene;
        
        bool canSetLevelSettings;

        public void SetPersistantObjectDestroyer(PersistantObjectDestroyer newObjectDestroyer)
        {
            persistantObjectDestroyer = newObjectDestroyer;
        }

        public void SetSceneToLoad(int newScene)
        {
            sceneToLoad = newScene;
        }

        public int GetSceneToLoad()
        {
            return sceneToLoad;
        }

        public int GetCurrentScene()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        public IEnumerator TransitionBeginCo()
        {
            previousScene = GetCurrentScene();
            transform.parent = null;
            GameObject.Find("AudioManager").transform.parent = null;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(GameObject.Find("AudioManager"));
            UIFade fader = FindObjectOfType<UIFade>();

            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
        }

        public IEnumerator TransitionEndCo()
        {
            UIFade fader = FindObjectOfType<UIFade>();
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            SetLevelSetChecker(true);
            if(GetCurrentScene() == 0 || GetCurrentScene() > lastPlayableScene)
            {
                if(GameObject.FindObjectOfType<Menu>() != null) GameObject.FindObjectOfType<Menu>().SetRetryLevel(previousScene);
                Debug.Log(GetCurrentScene());
                persistantObjectDestroyer.RestartSpawner();
                persistantObjectDestroyer.CheckIfPersistantMustBeDestroyed();
                Destroy(Singleton.instance.gameObject);
                Destroy(gameObject);
                Destroy(GameObject.Find("AudioManager"));
            } 
            else
            {
                transform.parent = Singleton.instance.transform;
                GameObject.Find("AudioManager").transform.parent = Singleton.instance.transform;
            }
            yield return new WaitForSeconds(0.1f);
            SetLevelSetChecker(false);
        }

        private void SetLevelSetChecker(bool checker)
        {
            canSetLevelSettings = checker;
        }

        public bool GetLevelSetChecker()
        {
            return canSetLevelSettings;
        }

    }
}
