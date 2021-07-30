using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;
using RPG.Combat;

namespace RPG.Core
{
    public class BossHealth : Health
    {
        [SerializeField] HealBar bar;

        private void Start()
        {
            ParentStartingSettings();
            // ShowVisualChanges();
        }

        public void SetHealthBar()
        {
            HealBar[] healbars = GameObject.FindObjectsOfType<HealBar>();
            for (int i = 0; i < healbars.Length; i++)
            {
                if (healbars[i].gameObject.name == "BossHealthBar")
                {
                    bar = healbars[i];
                    break;
                }
            }
        }
        
        public override void ShowVisualChanges()
        {
            bar.ChangeBarFiller(GetHP(), GetMaxHP());
        }

        public override void DeathBehaviour()
        {
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PlayerWeapon")
            {
                if (!CheckInvencibility())
                {
                    TakeDamage(other.transform.parent.GetComponent<AttackTrigger>().GetTriggerDamage());
                    SetInvencibility(true);
                    StartCoroutine(DisableInvencibilityCo(0.5f));
                }
            }
        }

    }
}