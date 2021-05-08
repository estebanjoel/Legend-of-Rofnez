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

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()//el codigo de animacion de caminata
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 LocalVelocity = transform.InverseTransformDirection(velocity);
            float speed = LocalVelocity.z;
            GetComponent<Animator>().SetFloat("forwordSpeed",speed);
        }


        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
    }
}
