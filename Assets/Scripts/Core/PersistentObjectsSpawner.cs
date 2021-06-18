using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.SceneManagement;

namespace RPG.Core
{
    public class PersistentObjectsSpawner : MonoBehaviour
    {    
        [SerializeField] GameObject persistentObjectPrefab;
        static bool hasSpawned = false;
        
        private void Awake()
        {
            if(hasSpawned) return;

            SpawnPersistentObject();
            hasSpawned = true;

        }

        public void RestartSpawnBool()
        {
            hasSpawned = false;
        }

        public void SpawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
            SceneLoader sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
            sceneLoader.SetPersistantObjectDestroyer(GameObject.FindObjectOfType<PersistantObjectDestroyer>());
        }

        public bool CheckIfPersistentObjectIsSpawned()
        {
            if(hasSpawned) return true;
            else return false;
        }
    }
}
