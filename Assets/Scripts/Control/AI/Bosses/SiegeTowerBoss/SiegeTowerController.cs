using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class SiegeTowerController : MonoBehaviour
    {
        [SerializeField] SiegeTowerPilotController siegeTowerPilotController;
        bool pilotOnTower;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private bool CheckIfPilotIsOnControl()
        {
            return false;
        }
    }

}