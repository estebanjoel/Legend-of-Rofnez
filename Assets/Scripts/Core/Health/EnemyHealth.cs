using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class EnemyHealth : Health
    {
        public override void ShowVisualChanges()
        {
            Debug.Log(GetHP());
        }

        public override void DeathBehaviour()
        {
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public override void HealShader()
        {
            throw new System.NotImplementedException();
        }
    }
}
