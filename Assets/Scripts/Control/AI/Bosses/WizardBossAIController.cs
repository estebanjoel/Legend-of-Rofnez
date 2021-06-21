using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Control
{
    public class WizardBossAIController : AIController
    {
        WizardBossHealth wizardBossHealth;
        [Header("Tornado Variables")]
        [SerializeField] GameObject tornadoVFX;
        bool canDoATornado;
        bool madeATornado;
        void Start()
        {
            ParentStartingSettings();
            wizardBossHealth = GetComponent<WizardBossHealth>();
        }
        
        public override void UpdateTimers()
        {
            // throw new System.NotImplementedException();
        }
        public override void UpdateBehaviour()
        {
            canDoATornado = wizardBossHealth.CheckIfICanDoATornadoMovement();
            if(canDoATornado)
            {
                if(!madeATornado)TornadoBehaviour();
            }
        }

        private void TornadoBehaviour()
        {
            Debug.Log("Tornado");
            madeATornado = false;
            GetHealth().SetInvencibility(true);
            GetHealth().SpawnShader(tornadoVFX);
        }

        public override void AttackBehaviour()
        {
            // throw new System.NotImplementedException();
        }
    }

}