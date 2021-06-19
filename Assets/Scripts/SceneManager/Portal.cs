using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneDestination = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] bool canBeUsed;
        [SerializeField] bool isOnPortal;

        private void Start()
        {
            if(!canBeUsed)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        public bool IsPlayerOnPortal()
        {
            return isOnPortal;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player" && canBeUsed)    
            {
                StartCoroutine(TransitionCo());
                isOnPortal = true;
            }
        }

        private IEnumerator TransitionCo()
        {
            SceneLoader sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
            sceneLoader.SetSceneToLoad(sceneDestination);
            DontDestroyOnLoad(gameObject);

            yield return sceneLoader.TransitionBeginCo();

            Portal otherPortal = GetOtherPortal();
            if(otherPortal != null) UpdatePlayer(otherPortal);
            yield return sceneLoader.TransitionEndCo();
            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) continue;
                return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }
}
