using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            
            if(GetCurrentScene() == 0 || GetCurrentScene() > lastPlayableScene) persistantObjectDestroyer.RestartSpawner();
            UIFade fader = FindObjectOfType<UIFade>();

            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
        }

        public IEnumerator TransitionEndCo()
        {
            UIFade fader = FindObjectOfType<UIFade>();
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            persistantObjectDestroyer.CheckIfPersistantMustBeDestroyed();
            if(GameObject.FindGameObjectWithTag("Player") == null) Destroy(gameObject);
            else transform.parent = Singleton.instance.transform;
        }

    }
}
