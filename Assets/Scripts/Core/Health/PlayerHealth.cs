using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class PlayerHealth : Health
    {
        HealBar bar;

        private void Start()
        {
            // SetStartingHealthSettings();
        }

        public void SetStartingHealthSettings()
        {
            ParentStartingSettings();
            HealBar[] healbars = GameObject.FindObjectsOfType<HealBar>();
            for (int i = 0; i < healbars.Length; i++)
            {
                if (healbars[i].gameObject.name == "Health")
                {
                    bar = healbars[i];
                    break;
                }
            }
            ShowVisualChanges();
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

        // public void HealActiv()
        // {
        //     healShader.gameObject.SetActive(true);
        // }
        // public void HealDiActiv()
        // {
        //     healShader.gameObject.SetActive(false);
        // }

        // public override void HealShader()
        // {
        //     HealActiv();
        //     StartCoroutine(DeactivateShader());
        // }

        // IEnumerator DeactivateShader()
        // {
        //     yield return new WaitForSeconds(2f);
        //     HealDiActiv();
        // }

    }
}
