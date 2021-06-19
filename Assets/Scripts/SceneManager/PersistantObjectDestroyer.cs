using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.SceneManagement
{
    public class PersistantObjectDestroyer : MonoBehaviour
    {
        public void CheckIfPersistantMustBeDestroyed()
        {
            if(GameObject.FindGameObjectWithTag("Player") == null || !GameObject.FindObjectOfType<PersistentObjectsSpawner>().CheckIfPersistentObjectIsSpawned())
            {
                Destroy(gameObject);
            }
        }

        public void RestartSpawner()
        {
            PersistentObjectsSpawner persistentObjectsSpawner = GameObject.FindObjectOfType<PersistentObjectsSpawner>();
            persistentObjectsSpawner.RestartSpawnBool();
        }
    }
}
