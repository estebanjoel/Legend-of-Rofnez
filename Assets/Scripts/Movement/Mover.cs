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
        [SerializeField] AudioSource runsound = null;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
            if(transform.position == navMeshAgent.destination && runsound != null) runsound.Stop();
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
            if(runsound != null) runsound.Play();
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
            // runsound.Stop();
            navMeshAgent.isStopped = true;
        }
    }
}
