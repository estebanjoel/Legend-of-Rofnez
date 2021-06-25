using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class PlayerHealth : Health
    {
        HealBar bar;

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
    }
}
