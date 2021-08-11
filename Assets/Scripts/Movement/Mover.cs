using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private Transform target;
        private NavMeshAgent navMeshAgent;
        private Health health;
        [Header("Audio Clips")]
        AudioManager audioManager;
        [SerializeField] AudioClip runClip;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
            audioManager = GameObject.FindObjectOfType<AudioManager>();
        }

        private void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
            if(transform.position == navMeshAgent.destination && runClip != null) audioManager.StopClipFromSources(audioManager.PlayerSFXSources, runClip);
        }

        private void UpdateAnimator()//el codigo de animacion de caminata
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 LocalVelocity = transform.InverseTransformDirection(velocity);
            float speed = LocalVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed",speed);
        }


        //Inicio la acción de moverme
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            if(runClip != null) audioManager.TryToPlayClip(audioManager.PlayerSFXSources, runClip);
            MoveTo(destination);
        }
        //Selecciono la posición de destino y empiezo a moverme
        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        //Cancela la acción de moverme
        public void Cancel()
        {
            if (runClip != null) audioManager.TryToPlayClip(audioManager.PlayerSFXSources, runClip);
            navMeshAgent.isStopped = true;
        }
    }
}
