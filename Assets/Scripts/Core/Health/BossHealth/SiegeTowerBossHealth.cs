using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.UI;

namespace RPG.Core
{
    public class SiegeTowerBossHealth : Health
    {
        [SerializeField] GameObject explosionVFX;
        [SerializeField] float hpAmountToActivateShield;
        float currentAmount = 0;
        int timesShieldWasActivated = 0;
        [Header("Health Bar")]
        [SerializeField] HealBar bar;

        void Start()
        {
            ParentStartingSettings();
            SetHealthBar();
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
        
        public override void DeathBehaviour()
        {
            StartCoroutine(ExplodeCo());
        }
        public override void ShowVisualChanges()
        {
            bar.ChangeBarFiller(GetHP(), GetMaxHP());
        }

        private void ChangeHPAmountToDoSomeMechanic()
        {
            currentAmount += GetMaxHP() - GetHP() - currentAmount - hpAmountToActivateShield * timesShieldWasActivated;
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

        private IEnumerator ExplodeCo()
        {
            float explosionYPos = transform.position.y + 1.75f;
            Vector3 explosionPos = new Vector3(transform.position.x, explosionYPos, transform.position.z);
            yield return new WaitForSeconds(5f);
            Instantiate(explosionVFX, explosionPos, transform.rotation);
            yield return new WaitForSeconds(0.25f);
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

    }
}
