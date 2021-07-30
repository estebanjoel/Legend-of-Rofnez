using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;

namespace RPG.Core
{
    public class ObstacleHealth : Health
    {
        public override void ShowVisualChanges()
        {
            Debug.Log(GetHP());
        }
        
        public override void DeathBehaviour()
        {
            GetComponent<NavMeshObstacle>().enabled = false;
            Destroy(gameObject);
            // GetComponent<MeshFilter>().mesh = null;
            // transform.GetChild(0).gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PlayerWeapon")
            {
                if(!CheckInvencibility())
                {
                    TakeDamage(other.transform.parent.GetComponent<AttackTrigger>().GetTriggerDamage());
                    SetInvencibility(true);
                    StartCoroutine(DisableInvencibilityCo(0.5f));
                }
            }
        }
    }
}
